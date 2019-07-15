package cn.rongcloud.imlib.unity;

import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.provider.MediaStore;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.JsonArray;
import com.google.gson.JsonObject;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import io.rong.imlib.RongIMClient;
import io.rong.imlib.RongIMClient.ErrorCode;
import io.rong.imlib.RongIMClient.OperationCallback;
import io.rong.imlib.RongIMClient.ResultCallback;
import io.rong.imlib.model.Conversation;
import io.rong.imlib.model.Conversation.ConversationNotificationStatus;
import io.rong.imlib.model.Message;
import io.rong.imlib.model.PublicServiceProfile;
import io.rong.imlib.model.PublicServiceProfileList;

import java.io.*;
import java.util.List;

import static cn.rongcloud.imlib.unity.Convert.toJSON;
import static cn.rongcloud.imlib.unity.Convert.toJson;

public class Utils {

    static void sendCommCallbackMessage(String callback, JsonObject msg){
        JsonObject data=new JsonObject();

        data.addProperty("callback", callback);
        data.add("data",msg);

        Gson gson=new Gson();
        UnityPlayer.UnitySendMessage("RongCloud","CommCallback",gson.toJson(data));
    }
    static OperationCallback createOperationCallback(final String callback){
        return new OperationCallback() {
            @Override
            public void onSuccess() {
                JsonObject msg=createJsonObject("status","success");
                sendCommCallbackMessage(callback,msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage(callback,createCommErrorMsg(errorCode));
            }
        };
    }

    private static String getPathFromUri(Context context, Uri uri, String fileName) {
        try {
            InputStream inputStream = context.getContentResolver().openInputStream(uri);
            File file = createTemporalFile(context, inputStream, fileName);
            if (file != null) {
                return file.getPath();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        return null;
    }

    private static File createTemporalFile(Context context, InputStream inputStream, String fileName) throws IOException {
        if (inputStream != null) {
            File target = new File(context.getCacheDir(), fileName);
            OutputStream outputStream = new FileOutputStream(target);
            try {
                byte[] buffer = new byte[1024];
                int read;
                while ((read = inputStream.read(buffer)) > 0) {
                    outputStream.write(buffer, 0, read);
                }
            } finally {
                outputStream.close();
                inputStream.close();
            }
            return target;
        }
        return null;
    }

    static Uri getFileUri(Context context, String s) {
        Uri uri = Uri.parse(s);
        if (s.startsWith("content://")) {
            String[] types = {MediaStore.MediaColumns.DATA, MediaStore.MediaColumns.DISPLAY_NAME};
            Cursor cursor = context.getContentResolver().query(uri, types, null, null, null);
            if (cursor != null) {
                cursor.moveToFirst();
                String path = cursor.getString(cursor.getColumnIndex(types[0]));
                String filename = cursor.getString(cursor.getColumnIndex(types[1]));
                cursor.close();
                if (path == null) {
                    path = Utils.getPathFromUri(context, uri, filename);
                }
                return Uri.parse("file://" + path);
            }
        }
        return uri;
    }
    static JsonObject createJsonObject(String key,String value) {
        JsonObject msg=new JsonObject();
        msg.addProperty(key,value);

        return msg;
    }
    static JsonArray toJsonArray(String str){
        Gson gson=new Gson();

        return gson.fromJson(str,JsonArray.class);

    }
    static ResultCallback<Boolean> createBooleanCallback(final String callback) {
        return new ResultCallback<Boolean>() {
            @Override
            public void onSuccess(Boolean result) {
                JsonObject msg=createJsonObject("status","success");
                msg.addProperty("result",result);
                sendCommCallbackMessage(callback,msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage(callback,createCommErrorMsg(errorCode));
            }
        };
    }
    static JsonObject toJsonObject(String json){
        Gson gson=new Gson();
        return gson.fromJson(json,JsonObject.class);
    }
    static JsonObject createCommErrorMsg(ErrorCode errorCode){
        JsonObject msg=createJsonObject("status","error");
        msg.addProperty("errorCode",errorCode.getValue());
        return msg;
    }
    static RongIMClient.ResultCallback<List<Message>> createMessagesCallback(final String callback) {
        return new RongIMClient.ResultCallback<List<Message>>() {
            @Override
            public void onSuccess(List<Message> messages) {
                JsonObject msg=createJsonObject("status","success");
                msg.add("messages",toJSON(messages));
                sendCommCallbackMessage(callback,msg);
            }

            @Override
            public void onError(RongIMClient.ErrorCode errorCode) {
                sendCommCallbackMessage(callback,createCommErrorMsg(errorCode));
            }


        };
    }
    static ResultCallback<List<Conversation>> createConversationListCallback(final String callback) {
        return new ResultCallback<List<Conversation>>() {
            @Override
            public void onSuccess(List<Conversation> conversations) {
                JsonObject msg=createJsonObject("status","success");

                JsonArray array = new JsonArray();
                if (conversations != null) {
                    for (Conversation conversation : conversations) {
                        array.add(toJson(conversation));
                    }
                }
                msg.add("conversations",array);
                sendCommCallbackMessage(callback,msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage(callback,createCommErrorMsg(errorCode));
            }
        };
    }
    static ResultCallback<ConversationNotificationStatus> createConversationNotificationStatusCallback(final String callback) {
        return new ResultCallback<ConversationNotificationStatus>() {
            @Override
            public void onSuccess(ConversationNotificationStatus status) {
                JsonObject msg=createJsonObject("status","success");
                msg.addProperty("NotificationStatus",status.getValue());
                sendCommCallbackMessage(callback,msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage(callback,createCommErrorMsg(errorCode));
            }
        };
    }
}

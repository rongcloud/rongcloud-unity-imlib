package cn.rongcloud.imlib.unity;

import android.content.Context;
import android.os.IBinder;
import android.os.RemoteException;
import android.util.Log;

import com.google.gson.Gson;

import org.json.JSONArray;
import org.json.JSONException;

import com.google.gson.JsonArray;
import com.google.gson.JsonObject;

import io.rong.imlib.model.*;
import io.rong.imlib.*;
import io.rong.imlib.RongIMClient.*;
import io.rong.imlib.model.ChatRoomInfo.*;

import static cn.rongcloud.imlib.unity.Convert.*;
import static cn.rongcloud.imlib.unity.Utils.*;

public class RongCloud {
    /**
     *
     */
    private RongCloud() {


    }
    public static void init(Context context,String appKey){
        Convert.unityContext=context;
        RongIMClient.init(context,appKey);
        RongIMClient.setConnectionStatusListener(new ConnectionStatusListener() {
            @Override
            public void onChanged(ConnectionStatus connectionStatus) {
                sendCommCallbackMessage("ConnectionStatus",createJsonObject("status",String.valueOf(connectionStatus.getValue())));
            }
        });

        RongIMClient.setOnReceiveMessageListener(new RongIMClient.OnReceiveMessageListener(){
            @Override
            public boolean onReceived(Message message, int left) {
                Gson gson=new Gson();
                JsonObject msg=new JsonObject();

                msg.add("message",toJson(message));
                msg.addProperty("left", left);

                sendCommCallbackMessage("OnReceiveMessageListerner",msg);
                return false;
            }
        });
    }
    public static void connect(String token){
        RongIMClient.connect(token,new RongIMClient.ConnectCallback(){
            /**
             * Token 错误。可以从下面两点检查 1.  Token 是否过期，如果过期您需要向 App Server 重新请求一个新的 Token
             *                            2.  token 对应的 appKey 和工程里设置的 appKey 是否一致
             */
            @Override
            public void onTokenIncorrect() {
                sendCommCallbackMessage("connect",createJsonObject("status","tokenIncorrect"));
            }

            /**
             * 连接融云成功
             * @param userid 当前 token 对应的用户 id
             */
            @Override
            public void onSuccess(String userid) {
                JsonObject msg=createJsonObject("status","success");
                msg.addProperty("userid",userid);
                sendCommCallbackMessage("connect",msg);
            }

            /**
             * 连接融云失败
             * @param errorCode 错误码，可到官网 查看错误码对应的注释
             */
            @Override
            public void onError(RongIMClient.ErrorCode errorCode) {
                JsonObject msg=createJsonObject("status","error");
                msg.addProperty("errorCode",errorCode.getValue());
                sendCommCallbackMessage("connect",msg);
            }
        });

    }

    public static String getCurrentUserId(){
        return RongIMClient.getInstance().getCurrentUserId();
    }
    public static int getCurrentConnectionStatus(){
        return RongIMClient.getInstance().getCurrentConnectionStatus().getValue();
    }
    public static void logout(){
        RongIMClient.getInstance().logout();
    }

    public static void sendMessage(String data,String eventId){


            Gson gson=new Gson();

            JsonObject msg=gson.fromJson(data,JsonObject.class);

            String pushContent=msg.get("pushContent").getAsString();
            String pushData=msg.get("pushData").getAsString();
            Message message=toMessage(msg);
            RongIMClient.getInstance().sendMessage(message,pushContent,pushData, createSendMessageCallback(eventId));


    }
    private static IRongCallback.ISendMessageCallback createSendMessageCallback(final String eventId) {
        return new IRongCallback.ISendMessageCallback() {


            @Override
            public void onAttached(Message message) {

            }

            @Override
            public void onSuccess(Message message) {
                JsonObject msg=createJsonObject("status","success");
                msg.addProperty("eventId",eventId);
                msg.add("message",toJson(message));
                sendCommCallbackMessage("SendMessageCallback", createJsonObject(eventId,"success"));
            }

            @Override
            public void onError(Message message, ErrorCode errorCode) {
                onSendMessageError(eventId,message,errorCode.getValue());
            }
        };
    }
    private static ISendMediaMessageCallback createSendMediaMessageCallback(final String eventId) {
        return new ISendMediaMessageCallback() {


            @Override
            public IBinder asBinder() {
                return null;
            }

            @Override
            public void onAttached(Message message) {


            }

            @Override
            public void onProgress(Message message, int i)  {
                JsonObject msg = createJsonObject(eventId, "progress");
                msg.addProperty("messageId", message.getMessageId());
                msg.addProperty("progress", i);
                sendCommCallbackMessage("SendMessageCallback",msg);
            }

            @Override
            public void onSuccess(Message message) {
                sendCommCallbackMessage("SendMessageCallback", createJsonObject(eventId,"success"));
            }

            @Override
            public void onError(Message message, int i)  {
                onSendMessageError(eventId,message,i);
            }

            @Override
            public void onCanceled(Message message)  {
                sendCommCallbackMessage("SendMessageCallback", createJsonObject(eventId,"cancel"));
            }


        };
    }
    private static void onSendMessageError(String eventId, Message message, int errorCode) {
        JsonObject msg=createJsonObject("status","error");
        msg.addProperty("eventId",eventId);
        msg.addProperty("errorCode",errorCode);
        if(message!=null){
            msg.add("message",toJson(message));
        }

        sendCommCallbackMessage("SendMessageCallback",msg);
    }
    public static void joinChatRoom(String targetId,int messageCount){
        RongIMClient.getInstance().joinChatRoom(targetId,messageCount,createOperationCallback("joinChatRoom"));
    }
    public static void getChatRoomInfo(String targetId, int memberCount, int order){
        ChatRoomMemberOrder memberOrder = order == ChatRoomMemberOrder.RC_CHAT_ROOM_MEMBER_ASC.getValue() ?
                ChatRoomMemberOrder.RC_CHAT_ROOM_MEMBER_ASC : ChatRoomMemberOrder.RC_CHAT_ROOM_MEMBER_DESC;
        RongIMClient.getInstance().getChatRoomInfo(targetId, memberCount, memberOrder, new ResultCallback<ChatRoomInfo>() {
            @Override
            public void onSuccess(ChatRoomInfo chatRoomInfo) {
                JsonObject msg=createJsonObject("status","success");
                msg.addProperty("targetId", chatRoomInfo.getChatRoomId());
                msg.addProperty("totalMemberCount",chatRoomInfo.getTotalMemberCount());
                msg.addProperty("memberOrder",chatRoomInfo.getMemberOrder().getValue());
                JsonArray array=new JsonArray();
                for(ChatRoomMemberInfo member:chatRoomInfo.getMemberInfo()){
                    JsonObject obj=new JsonObject();
                    obj.addProperty("userId",member.getUserId());
                    obj.addProperty("joinTime",member.getJoinTime());
                    array.add(obj);
                }
                msg.add("members",array);
                sendCommCallbackMessage("getChatRoomInfo",msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                JsonObject msg=createJsonObject("status","error");
                msg.addProperty("errorCode",errorCode.getValue());
                sendCommCallbackMessage("getChatRoomInfo",msg);
            }
        });
    }


}

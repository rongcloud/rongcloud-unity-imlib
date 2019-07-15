package cn.rongcloud.imlib.unity;

import android.content.Context;
import android.os.IBinder;
import android.os.RemoteException;
import android.util.Log;

import com.google.gson.Gson;

import com.google.gson.JsonArray;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;

import org.json.JSONArray;

import java.util.*;

import io.rong.imlib.model.*;
import io.rong.imlib.model.Conversation.*;
import io.rong.imlib.*;
import io.rong.imlib.RongIMClient.*;
import io.rong.imlib.model.ChatRoomInfo.*;
import io.rong.imlib.typingmessage.TypingStatus;
import io.rong.message.RecallNotificationMessage;
import io.rong.imlib.RongCommonDefine.*;
import io.rong.imlib.CustomServiceConfig.*;
import io.rong.imlib.location.*;
import io.rong.imlib.location.RealTimeLocationConstant.*;

import static cn.rongcloud.imlib.unity.Convert.*;
import static cn.rongcloud.imlib.unity.Utils.*;

public class RongCloud {
    final  static String RC_STATUS="status";
    final static String RC_SUCCESS="success";
    final static String RC_ERROR="error";
    /**
     *
     */ RongCloud() {


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

        RongIMClient.setTypingStatusListener(new TypingStatusListener() {
            @Override
            public void onTypingStatusChanged(ConversationType conversationType, String targetId, Collection<TypingStatus> set) {
               JsonObject msg=new JsonObject();
                msg.addProperty("conversationType", conversationType.getValue());
                msg.addProperty("targetId", targetId);
                JsonArray jsonArray=new JsonArray();

                for(TypingStatus status:set) {

                    JsonObject item=new JsonObject();
                    item.addProperty("userId", status.getUserId());
                    item.addProperty("sentTime", status.getSentTime());
                    item.addProperty("typingContentType", status.getTypingContentType());
                    jsonArray.add(item);
                }
                msg.add("statusList",jsonArray);
                sendCommCallbackMessage("onTypingStatusChanged",msg);
            }
        });

        RongIMClient.setReadReceiptListener(new ReadReceiptListener() {
            @Override
            public void onReadReceiptReceived(Message message) {
                JsonObject msg=createJsonObject("type","onReadReceiptReceived");
                msg.add("message", toJson(message));
                sendCommCallbackMessage("setReadReceiptListener",msg);
            }

            @Override
            public void onMessageReceiptRequest(ConversationType conversationType, String targetId, String UId) {
                JsonObject msg=createJsonObject("type","onMessageReceiptRequest");
                msg.addProperty("conversationType", conversationType.getValue());
                msg.addProperty("targetId", targetId);
                msg.addProperty("messageUId", UId);
                sendCommCallbackMessage("setReadReceiptListener",msg);

            }

            @Override
            public void onMessageReceiptResponse(ConversationType conversationType, String targetId, String UId, HashMap<String, Long> users) {
                JsonObject msg=createJsonObject("type","onMessageReceiptResponse");
                msg.addProperty("conversationType", conversationType.getValue());
                msg.addProperty("targetId", targetId);
                msg.addProperty("messageUId", UId);
                JsonObject userIdList=new JsonObject();
                for (String userId : users.keySet()) {
                    Long readTime = users.get(userId);
                    if (readTime != null) {
                        userIdList.addProperty(userId, readTime);
                    }
                }
                msg.add("usesIdList", userIdList);
                sendCommCallbackMessage("setReadReceiptListener",msg);
            }
        });

        RongIMClient.setRCLogInfoListener(new RCLogInfoListener() {
            @Override
            public void onRCLogInfoOccurred(String log) {
                sendCommCallbackMessage("setRCLogInfoListener",createJsonObject("log",log));
            }
        });

        RongIMClient.setOnRecallMessageListener(new OnRecallMessageListener() {
            @Override
            public boolean onMessageRecalled(Message message, RecallNotificationMessage recall) {
                JsonObject msg=new JsonObject();
                msg.addProperty("messageId",message.getMessageId());
                sendCommCallbackMessage("setOnRecallMessageListener",msg);
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

        try{
            Gson gson=new Gson();

            JsonObject msg=gson.fromJson(data,JsonObject.class);

            String pushContent = getStringFromJson(msg, "pushContent");
            String pushData = getStringFromJson(msg,  "pushData");
            Message message=toMessage(msg);
            RongIMClient.getInstance().sendMessage(message,pushContent,pushData, createSendMediaMessageCallback(eventId));
        } catch (Exception e) {
            onSendMessageError(eventId, null, ErrorCode.PARAMETER_ERROR.getValue());
        }

    }


    private static IRongCallback.ISendMediaMessageCallback createSendMediaMessageCallback(final String eventId) {
        return new IRongCallback.ISendMediaMessageCallback() {


            @Override
            public void onAttached(Message message) {


            }

            @Override
            public void onProgress(Message message, int i)  {
                JsonObject msg = createJsonObject("status", "progress");
                msg.addProperty("eventId",eventId);
                msg.addProperty("messageId",message.getMessageId());
                msg.addProperty("progress", i);
                sendCommCallbackMessage("SendMessageCallback",msg);
            }

            @Override
            public void onSuccess(Message message) {
                JsonObject msg=createJsonObject("status","success");
                msg.addProperty("eventId",eventId);
                msg.addProperty("messageId",message.getMessageId());
                sendCommCallbackMessage("SendMessageCallback", msg);
        }

            @Override
            public void onError(Message message, ErrorCode errorCode) {
                onSendMessageError(eventId,message,errorCode.getValue());
            }


            @Override
            public void onCanceled(Message message)  {
                JsonObject msg=createJsonObject("status","cancel");
                msg.addProperty("eventId",eventId);
                msg.addProperty("messageId",message.getMessageId());
                sendCommCallbackMessage("SendMessageCallback", msg);
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
    static String getStringFromJson(JsonObject json,String key){
        String value = null;
        if (json.has(key)) {
            value = json.get(key).getAsString();
            if(value=="")
                value=null;
        }
        return value;
    }


    public static void disconnect(){
        RongIMClient.getInstance().disconnect();
    }
    public static void sendMediaMessage(String data,String eventId) {
        try {
            Gson gson=new Gson();
            JsonObject msg=gson.fromJson(data,JsonObject.class);
            String pushContent=msg.get("pushContent").getAsString();
            String pushData=msg.get("pushData").getAsString();
            Message message=toMessage(msg);
            RongIMClient.getInstance().sendMediaMessage(message, pushContent, pushData, createSendMediaMessageCallback(eventId));
        } catch (Exception e) {
            onSendMessageError(eventId, null, ErrorCode.PARAMETER_ERROR.getValue());
        }
    }
    public static void sendDirectionalMessage(String data, final String eventId) {
        try {
            Gson gson=new Gson();
            JsonObject msg=gson.fromJson(data,JsonObject.class);
            ConversationType conversationType = ConversationType.setValue(msg.get("conversationType").getAsInt());
            MessageContent messageContent = toMessageContent(toJsonObject(msg.get("content").getAsString()) );
            String targetId = msg.get("targetId").getAsString();
            String pushContent = getStringFromJson(msg, "pushContent");
            String pushData = getStringFromJson(msg,  "pushData");
            String[] array = toStringArray(toJsonArray(msg.get("userIds").getAsString()) );
            RongIMClient.getInstance().sendDirectionalMessage(
                    conversationType, targetId, messageContent, array, pushContent, pushData, createSendMediaMessageCallback(eventId));
       } catch (Exception e) {
            onSendMessageError(eventId, null, ErrorCode.PARAMETER_ERROR.getValue());
        }
    }
    public static void sendMessage(String data,boolean option,String eventId){
         SendMessageOption sendMessageOption=new SendMessageOption();
         sendMessageOption.setVoIPPush(option);
        try {
            Gson gson=new Gson();
            JsonObject msg=gson.fromJson(data,JsonObject.class);
            String pushContent=msg.get("pushContent").getAsString();
            String pushData=msg.get("pushData").getAsString();
            Message message=toMessage(msg);
            RongIMClient.getInstance().sendMessage(message, pushContent, pushData,sendMessageOption, createSendMediaMessageCallback(eventId));
        } catch (Exception e) {
            onSendMessageError(eventId, null, ErrorCode.PARAMETER_ERROR.getValue());
        }

    }
    public static void insertIncomingMessage(String data,long sentTime){
        try {
            Gson gson=new Gson();
            JsonObject msg=gson.fromJson(data,JsonObject.class);
            ConversationType conversationType = ConversationType.setValue(msg.get("conversationType").getAsInt());
            MessageContent messageContent = toMessageContent(toJsonObject(msg.get("content").getAsString()) );
            String targetId = msg.get("targetId").getAsString();
            String sendUserId=msg.get("senderUserId").getAsString();
            Message.ReceivedStatus rs=new Message.ReceivedStatus(msg.get("receivedStatus").getAsInt());

            String pushContent = getStringFromJson(msg, "pushContent");
            String pushData = getStringFromJson(msg,  "pushData");
            if(sentTime<=0){
                RongIMClient.getInstance().insertIncomingMessage(conversationType, targetId, sendUserId, rs, messageContent, new ResultCallback<Message>() {
                    @Override
                    public void onSuccess(Message message) {
                        Gson gson=new Gson();
                        JsonObject msg=new JsonObject();
                        msg.addProperty(RC_STATUS,RC_SUCCESS);
                        msg.add("message",toJson(message));
                        sendCommCallbackMessage("insertIncomingMessage",msg);
                    }

                    @Override
                    public void onError(ErrorCode errorCode) {
                        sendCommCallbackMessage("insertIncomingMessage",createCommErrorMsg(errorCode));
                    }
                });
            }else {
                RongIMClient.getInstance().insertIncomingMessage(conversationType,targetId,sendUserId,rs,messageContent,sentTime,new ResultCallback<Message>() {
                    @Override
                    public void onSuccess(Message message) {
                        Gson gson=new Gson();
                        JsonObject msg=new JsonObject();
                        msg.addProperty(RC_STATUS,RC_SUCCESS);
                        msg.add("message",toJson(message));
                        sendCommCallbackMessage("insertIncomingMessage",msg);
                    }

                    @Override
                    public void onError(ErrorCode errorCode) {
                        sendCommCallbackMessage("insertIncomingMessage",createCommErrorMsg(errorCode));
                    }
                });
            }
        } catch (Exception e) {
            sendCommCallbackMessage("insertIncomingMessage",createCommErrorMsg(ErrorCode.PARAMETER_ERROR));
        }
    }
    public static void insertOutgoingMessage(String data,long sentTime){
         try {
             Gson gson = new Gson();
             JsonObject msg = gson.fromJson(data, JsonObject.class);
             ConversationType conversationType = ConversationType.setValue(msg.get("conversationType").getAsInt());
             MessageContent messageContent = toMessageContent(toJsonObject(msg.get("content").getAsString()));
             String targetId = msg.get("targetId").getAsString();

             if (sentTime <= 0) {
                 RongIMClient.getInstance().insertOutgoingMessage(conversationType, targetId, Message.SentStatus.setValue(msg.get("sentStatus").getAsInt()), messageContent, new ResultCallback<Message>() {
                     @Override
                     public void onSuccess(Message message) {
                         Gson gson = new Gson();
                         JsonObject msg = new JsonObject();
                         msg.addProperty(RC_STATUS, RC_SUCCESS);
                         msg.add("message", toJson(message));
                         sendCommCallbackMessage("insertOutgoingMessage", msg);
                     }

                     @Override
                     public void onError(ErrorCode errorCode) {
                         sendCommCallbackMessage("insertOutgoingMessage", createCommErrorMsg(errorCode));
                     }
                 });
             } else {
                 RongIMClient.getInstance().insertOutgoingMessage(conversationType, targetId, Message.SentStatus.setValue(msg.get("sentStatus").getAsInt()), messageContent, sentTime, new ResultCallback<Message>() {
                     @Override
                     public void onSuccess(Message message) {
                         Gson gson = new Gson();
                         JsonObject msg = new JsonObject();
                         msg.addProperty(RC_STATUS, RC_SUCCESS);
                         msg.add("message", toJson(message));
                         sendCommCallbackMessage("insertOutgoingMessage", msg);
                     }

                     @Override
                     public void onError(ErrorCode errorCode) {
                         sendCommCallbackMessage("insertOutgoingMessage", createCommErrorMsg(errorCode));
                     }
                 });
             }
         }catch (Exception e) {
            sendCommCallbackMessage("insertOutgoingMessage",createCommErrorMsg(ErrorCode.PARAMETER_ERROR));
        }
    }
    public static void clearMessages(int type, String targetId) {
        RongIMClient.getInstance().clearMessages(
                ConversationType.setValue(type), targetId, createBooleanCallback("clearMessages"));
    }

    public static void deleteMessages(int type, String targetId) {
        RongIMClient.getInstance().deleteMessages(
                ConversationType.setValue(type), targetId, createBooleanCallback("deleteMessages"));
    }

    public static void deleteMessagesByIds(String ids) {
        Gson gson=new Gson();
        JsonArray msg=gson.fromJson(ids,JsonObject.class).getAsJsonArray();
        int[] array = new int[msg.size()];
        for (int i = 0; i < msg.size(); i += 1) {
            array[i] = msg.get(i).getAsInt();
        }
        RongIMClient.getInstance().deleteMessages(array, createBooleanCallback("deleteMessagesByIds"));
    }

    public static void deleteRemoteMessages(int type, String targetId, String messages) {
        Gson gson=new Gson();
        JsonArray msg=gson.fromJson(messages,JsonObject.class).getAsJsonArray();

        Message[] array = new Message[msg.size()];
        for (int i = 0; i < msg.size(); i += 1) {
            JsonObject message = msg.get(i).getAsJsonObject();
            if (message == null) {
                array[i] = null;
            } else {
                array[i] = toMessage(message);
            }
        }
        RongIMClient.getInstance().deleteRemoteMessages(
                ConversationType.setValue(type), targetId, array, createOperationCallback("deleteRemoteMessages"));
    }
    public static void cleanHistoryMessages(int conversationType, String targetId, double timestamp, boolean clearRemote) {
        RongIMClient.getInstance().cleanHistoryMessages(
                ConversationType.setValue(conversationType), targetId, (long) timestamp, clearRemote, createOperationCallback("cleanHistoryMessages"));
    }

    public static void getHistoryMessages(
            int type, String targetId, String objectName, int oldestMessageId, int count, boolean isForward) {
        if (objectName == null || objectName.isEmpty()) {
            RongIMClient.getInstance().getHistoryMessages(
                    ConversationType.setValue(type), targetId, oldestMessageId, count, createMessagesCallback("getHistoryMessages"));
        } else {
            GetMessageDirection direction = isForward ? GetMessageDirection.FRONT : GetMessageDirection.BEHIND;
            RongIMClient.getInstance().getHistoryMessages(
                    ConversationType.setValue(type), targetId, objectName, oldestMessageId, count, direction, createMessagesCallback("getHistoryMessages"));
        }
    }
    public static void getHistoryMessages(int type,String targetId,long sentTime,int before,int after){
         RongIMClient.getInstance().getHistoryMessages(ConversationType.setValue(type),targetId,sentTime,before,after,createMessagesCallback("getHistoryMessages"));
    }
    public static void getHistoryMessagesByTimestamp(
            int type, String targetId, String objectNames, double timestamp, int count, boolean isForward) {
        Gson gson=new Gson();
        JsonArray msg=gson.fromJson(objectNames,JsonObject.class).getAsJsonArray("objectNames");
        if (objectNames == null || msg.size() == 0) {
            RongIMClient.getInstance().getHistoryMessages(
                    ConversationType.setValue(type), targetId, (long) timestamp, count, 0, createMessagesCallback("getHistoryMessagesByTimestamp"));
        } else {
            GetMessageDirection direction = isForward ? GetMessageDirection.FRONT : GetMessageDirection.BEHIND;
            ArrayList<String> names = Convert.toStringList(msg);
            RongIMClient.getInstance().getHistoryMessages(
                    ConversationType.setValue(type), targetId, names, (long) timestamp, count, direction, createMessagesCallback("getHistoryMessagesByTimestamp"));
        }
    }

    public static void getRemoteHistoryMessages(int type, String targetId, double sentTime, int count) {
        ResultCallback<List<Message>> callback = createMessagesCallback("getRemoteHistoryMessages");
        ConversationType conversationType = ConversationType.setValue(type);
        RongIMClient.getInstance().getRemoteHistoryMessages(conversationType, targetId, (long) sentTime, count, callback);
    }
    public static void cleanRemoteHistoryMessages(int conversationType, String targetId, double timestamp) {
        RongIMClient.getInstance().cleanRemoteHistoryMessages(
                ConversationType.setValue(conversationType), targetId, (long) timestamp, createOperationCallback("cleanRemoteHistoryMessages"));
    }


    public static void searchConversations(String keyword, String data) {
        final JsonObject msg=toJsonObject((data));
        Gson gson=new Gson();
        ConversationType[] conversationTypes = toConversationTypeArray(gson.fromJson(msg.get("types").getAsString(),JsonArray.class));
        String[] objectNamesArray = toStringArray(gson.fromJson(msg.get("objectNames").getAsString(),JsonArray.class));
        RongIMClient.getInstance().searchConversations(
                keyword, conversationTypes, objectNamesArray, new ResultCallback<List<SearchConversationResult>>() {
                    @Override
                    public void onSuccess(List<SearchConversationResult> conversations) {
                        JsonObject result=createJsonObject(RC_STATUS,RC_SUCCESS);
                        JsonArray convers=new JsonArray();
                        for (SearchConversationResult item : conversations) {
                            JsonObject map =new JsonObject();
                            map.add("conversation", toJson(item.getConversation()));
                            map.addProperty("matchCount", item.getMatchCount());
                            convers.add(map);
                        }
                        result.add("conversations",convers);
                        sendCommCallbackMessage("searchConversations",result);
                    }

                    @Override
                    public void onError(RongIMClient.ErrorCode errorCode) {
                        sendCommCallbackMessage("searchConversations",createCommErrorMsg(errorCode));
                    }
                });
    }

    public static void searchMessages(int conversationType, String targetId, String keyword, int count, long startTime) {
        ResultCallback<List<Message>> callback = createMessagesCallback("searchMessages");
        RongIMClient.getInstance().searchMessages(
                ConversationType.setValue(conversationType), targetId, keyword, count,  startTime, callback);
    }

    public static void searchMessagesByUser(int conversationType, String targetId,String userId, int count, long startTime) {
        ResultCallback<List<Message>> callback = createMessagesCallback("searchMessages");
        RongIMClient.getInstance().searchMessagesByUser(
                ConversationType.setValue(conversationType), targetId, userId, count,  startTime, callback);
    }

    public static void clearMessagesUnreadStatus(int conversationType, String targetId, long time) {
        if (time == 0) {
            RongIMClient.getInstance().clearMessagesUnreadStatus(
                    ConversationType.setValue(conversationType), targetId, createBooleanCallback("clearMessagesUnreadStatus"));
        } else {
            RongIMClient.getInstance().clearMessagesUnreadStatus(
                    ConversationType.setValue(conversationType), targetId, time,createOperationCallback("clearMessagesUnreadStatus"));
        }
    }


/*
        private static final int READ = 1;
        private static final int LISTENED = 2;
        private static final int DOWNLOADED = 4;
        private static final int RETRIEVED = 8;
        private static final int MULTIPLERECEIVE = 16;
 */
    public static void setMessageReceivedStatus(int messageId,int receivedStatus){
         RongIMClient.getInstance().setMessageReceivedStatus(messageId, new Message.ReceivedStatus(receivedStatus),createBooleanCallback("setMessageReceivedStatus"));
    }

    public static void getConversation(int conversationType, String targetId) {
        RongIMClient.getInstance().getConversation(
                ConversationType.setValue(conversationType), targetId, new ResultCallback<Conversation>() {
                    @Override
                    public void onSuccess(Conversation conversation) {
                        JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                        msg.add("conversation",toJson(conversation));
                       sendCommCallbackMessage("getConversation",msg);
                    }

                    @Override
                    public void onError(ErrorCode errorCode) {
                        sendCommCallbackMessage("getConversation",createCommErrorMsg(errorCode));
                    }
                });
    }


    public static void getConversationList(String conversationTypes, int count, long timestamp) {
        ConversationType[] types = toConversationTypeArray(toJsonArray(conversationTypes));
        ResultCallback<List<Conversation>> callback = createConversationListCallback("getConversationList");
        if (types.length > 0) {
            if (count > 0) {
                RongIMClient.getInstance().getConversationListByPage(callback, timestamp, count, types);
            } else {
                RongIMClient.getInstance().getConversationList(callback, types);
            }
        } else {
            RongIMClient.getInstance().getConversationList(callback);
        }
    }

    public static void removeConversation(int conversationType,String targetId){
        RongIMClient.getInstance().removeConversation(ConversationType.setValue(conversationType),targetId,createBooleanCallback("removeConversation"));
    }

    public static void setConversationNotificationStatus(int conversationType, String targetId, boolean isBlock) {
        RongIMClient.getInstance().setConversationNotificationStatus(
                ConversationType.setValue(conversationType),
                targetId,
                isBlock ? ConversationNotificationStatus.DO_NOT_DISTURB : ConversationNotificationStatus.NOTIFY,
                createConversationNotificationStatusCallback("setConversationNotificationStatus"));
    }

    public static void getConversationNotificationStatus(int conversationType, String targetId) {
        RongIMClient.getInstance().getConversationNotificationStatus(
                ConversationType.setValue(conversationType), targetId, createConversationNotificationStatusCallback("getConversationNotificationStatus"));
    }

    public static void setNotificationQuietHours(final String startTime, final int spanMinutes){
        RongIMClient.getInstance().setNotificationQuietHours(startTime,spanMinutes,createOperationCallback("setNotificationQuietHours"));
    }

    public static void removeNotificationQuietHours(){
        RongIMClient.getInstance().removeNotificationQuietHours(createOperationCallback("removeNotificationQuietHours"));
    }

    public static void saveTextMessageDraft(int conversationType, String targetId, String content) {
        RongIMClient.getInstance().saveTextMessageDraft(
                ConversationType.setValue(conversationType), targetId, content, createBooleanCallback("saveTextMessageDraft"));
    }

    public static void getTextMessageDraft(int conversationType, String targetId) {
        RongIMClient.getInstance().getTextMessageDraft(
                ConversationType.setValue(conversationType), targetId, new ResultCallback<String>() {
                    @Override
                    public void onSuccess(String content) {
                        JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                        msg.addProperty("content",content);
                        sendCommCallbackMessage("getTextMessageDraft",msg);
                    }
                    @Override
                    public void onError(ErrorCode errorCode) {
                        sendCommCallbackMessage("getTextMessageDraft",createCommErrorMsg(errorCode));
                    }
                });
    }

    public static void setConversationToTop(int conversationType, String targetId, boolean  isTop) {
        RongIMClient.getInstance().setConversationToTop(
                ConversationType.setValue(conversationType), targetId, isTop, createBooleanCallback("setConversationToTop"));
    }

    public static void getTotalUnreadCount() {
        RongIMClient.getInstance().getTotalUnreadCount(new ResultCallback<Integer>() {
            @Override
            public void onSuccess(Integer count) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                msg.addProperty("count",count);
                sendCommCallbackMessage("getTotalUnreadCount",msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("getTotalUnreadCount",createCommErrorMsg(errorCode));
            }
        });
    }
    public static void getTotalUnreadCount(String types) {
        Gson gson=new Gson();
        JsonArray conversations=gson.fromJson(types,JsonArray.class);
        Conversation[] cons=new Conversation[conversations.size()];
        for (int i = 0; i < conversations.size(); i += 1) {
            JsonObject obj=toJsonObject( conversations.get(i).getAsString());
            cons[i] = Conversation.obtain(ConversationType.setValue(obj.get("conversationType").getAsInt()),obj.get("targetId").getAsString(),obj.get("conversationTitle").getAsString());
        }
        RongIMClient.getInstance().getTotalUnreadCount( new ResultCallback<Integer>() {
            @Override
            public void onSuccess(Integer count) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                msg.addProperty("count",count);
                sendCommCallbackMessage("getTotalUnreadCount",msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("getTotalUnreadCount",createCommErrorMsg(errorCode));
            }
        }, cons);
    }
    public static void getUnreadCount(int conversationType, String targetId, String conversationTypes) {
        ResultCallback<Integer> callback = new ResultCallback<Integer>() {
            @Override
            public void onSuccess(Integer count) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                msg.addProperty("count",count);
                sendCommCallbackMessage("getUnreadCount",msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("getUnreadCount",createCommErrorMsg(errorCode));
            }
        };
        if (conversationType == 0) {
            ConversationType[] types = toConversationTypeArray(toJsonArray(conversationTypes));
            RongIMClient.getInstance().getUnreadCount(callback, types);
        } else {
            RongIMClient.getInstance().getUnreadCount(ConversationType.setValue(conversationType), targetId, callback);
        }
    }
    public static void getUnreadCount(String conversationTypes,boolean containBlocked) {
        ResultCallback<Integer> callback = new ResultCallback<Integer>() {
            @Override
            public void onSuccess(Integer count) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                msg.addProperty("count",count);
                sendCommCallbackMessage("getUnreadCount",msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("getUnreadCount",createCommErrorMsg(errorCode));
            }
        };
        ConversationType[] types = toConversationTypeArray(toJsonArray(conversationTypes));
        RongIMClient.getInstance().getUnreadCount(types,containBlocked,callback);
    }

    public static void addToBlacklist(String userId){
        RongIMClient.getInstance().addToBlacklist(userId,createOperationCallback("addToBlacklist"));
    }

    public static void removeFromBlacklist(String userId){
        RongIMClient.getInstance().removeFromBlacklist(userId,createOperationCallback("removeFromBlacklist"));
    }

    public static void getBlacklistStatus(final String userId){
        RongIMClient.getInstance().getBlacklistStatus(userId, new ResultCallback<BlacklistStatus>() {
            @Override
            public void onSuccess(BlacklistStatus blacklistStatus) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                msg.addProperty("BlacklistStatus",blacklistStatus.getValue());
                sendCommCallbackMessage("getBlacklistStatus",msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("getBlacklistStatus",createCommErrorMsg(errorCode));
            }
        });
    }

    public static void getBlacklist() {
        RongIMClient.getInstance().getBlacklist(new GetBlacklistCallback() {
            @Override
            public void onSuccess(String[] strings) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);

                JsonArray array=new JsonArray();
                for (String string : strings) {
                    array.add(string);
                }
                msg.add("ids",array );
                sendCommCallbackMessage("getBlacklist",msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("getBlacklist",createCommErrorMsg(errorCode));
            }
        });
    }
    public static void joinExistChatRoom(String targetId, int messageCount) {
        RongIMClient.getInstance().joinExistChatRoom(targetId, messageCount, createOperationCallback("joinExistChatRoom"));
    }

    public static void quitChatRoom(String targetId) {
        RongIMClient.getInstance().quitChatRoom(targetId, createOperationCallback("quitChatRoom"));
    }

    public static void getChatroomHistoryMessages(String targetId, double recordTime, int count, int order) {
        TimestampOrder timestampOrder = order == 0 ? TimestampOrder.RC_TIMESTAMP_DESC : TimestampOrder.RC_TIMESTAMP_ASC;
        RongIMClient.getInstance().getChatroomHistoryMessages(
                targetId, (long) recordTime, count, timestampOrder, new IRongCallback.IChatRoomHistoryMessageCallback() {

                    @Override
                    public void onSuccess(List<Message> list, long syncTime) {
                        JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                        msg.add("messages",toJSON(list));
                        msg.addProperty("syncTime", syncTime);
                        sendCommCallbackMessage("getChatroomHistoryMessages",msg);
                    }

                    @Override
                    public void onError(RongIMClient.ErrorCode errorCode) {
                        sendCommCallbackMessage("getChatroomHistoryMessages",createCommErrorMsg(errorCode));
                    }
                });
    }

    public static void startCustomerService(String kefuId, String kefuInfo) {
        RongIMClient.getInstance().startCustomService(kefuId, new ICustomServiceListener() {
            @Override
            public void onSuccess(CustomServiceConfig customServiceConfig) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                msg.add("config",toJson(customServiceConfig));
                sendCommCallbackMessage("startCustomerService",msg);
            }

            @Override
            public void onError(int errorCode, String message) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_ERROR);
                msg.addProperty("errorMessage",message);
                msg.addProperty("errorCode",errorCode);
                sendCommCallbackMessage("startCustomerService",msg);

            }

            @Override
            public void onModeChanged(CustomServiceMode customServiceMode) {
                JsonObject msg=createJsonObject(RC_STATUS,"onModeChanged");
                msg.addProperty("mode",customServiceMode.getValue());
                sendCommCallbackMessage("startCustomerService",msg);
            }

            @Override
            public void onQuit(String message) {
                JsonObject msg=createJsonObject(RC_STATUS,"onQuit");
                msg.addProperty("message", message);
                sendCommCallbackMessage("startCustomerService",msg);
            }

            @Override
            public void onPullEvaluation(String dialogId) {
                JsonObject msg=createJsonObject(RC_STATUS,"onPullEvaluation");
                msg.addProperty("dialogId", dialogId);
                sendCommCallbackMessage("startCustomerService",msg);
            }

            @Override
            public void onSelectGroup(List<CSGroupItem> list) {
                JsonObject msg=createJsonObject(RC_STATUS,"onSelectGroup");
                JsonArray groups =new JsonArray();
                for (CSGroupItem item : list) {
                    JsonObject group = new JsonObject();
                    group.addProperty("id", item.getId());
                    group.addProperty("name", item.getName());
                    group.addProperty("isOnline", item.getOnline());
                }
                msg.add("groups", groups);
                sendCommCallbackMessage("startCustomerService",msg);
            }
        }, toCSCustomServiceInfo(toJsonObject(kefuInfo)));
    }

    public static void switchToHumanMode(String kefuId) {
        RongIMClient.getInstance().switchToHumanMode(kefuId);
    }

    public static void selectCustomerServiceGroup(String kefuId, String groupId) {
        RongIMClient.getInstance().selectCustomServiceGroup(kefuId, groupId);
    }

    public static void evaluateCustomService(String kefuId, boolean isRobotResolved, String knowledgeId){
        RongIMClient.getInstance().evaluateCustomService(kefuId,isRobotResolved,knowledgeId);
    }
    public static void evaluateCustomService(String kefuId,
                                             int source,
                                             int resolvestatus,
                                             String tagText,
                                            String suggest,
                                             String dialogId,
                                             String extra){
        RongIMClient.getInstance().evaluateCustomService(kefuId,source,CSEvaSolveStatus.valueOf(resolvestatus),tagText,suggest,dialogId,extra);
    }
    public static void evaluateCustomService(String kefuId, int source, int solveStatus, String suggest, String dialogId){
        RongIMClient.getInstance().evaluateCustomService(kefuId,source,CSEvaSolveStatus.valueOf(solveStatus),suggest,dialogId);
    }
    public static void stopCustomerService(String kefuId) {
        RongIMClient.getInstance().stopCustomService(kefuId);
    }


    public static void getUnreadMentionedMessages(int conversationType, String targetId) {
        RongIMClient.getInstance().getUnreadMentionedMessages(
                ConversationType.setValue(conversationType), targetId, createMessagesCallback("getUnreadMentionedMessages"));
    }

    public static void recallMessage(int id, final String pushContent) {
        RongIMClient.getInstance().getMessage(id, new ResultCallback<Message>() {
            @Override
            public void onSuccess(Message message) {
                RongIMClient.getInstance().recallMessage(message, pushContent, new ResultCallback<RecallNotificationMessage>() {
                    @Override
                    public void onSuccess(RecallNotificationMessage message) {
                        JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                        msg.add("content",toJson("RC:RcNtf", message));
                        sendCommCallbackMessage("recallMessage",msg);
                    }

                    @Override
                    public void onError(ErrorCode errorCode) {
                        sendCommCallbackMessage("recallMessage",createCommErrorMsg(errorCode));
                    }
                });
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("recallMessage",createCommErrorMsg(errorCode));
            }
        });
    }

    public static void sendReadReceiptMessage(int conversationType, String targetId, double timestamp) {
        RongIMClient.getInstance().sendReadReceiptMessage(ConversationType.setValue(conversationType), targetId, (long) timestamp);
    }

    public static void sendReadReceiptRequest(int messageId) {
        RongIMClient.getInstance().getMessage(messageId, new ResultCallback<Message>() {
            @Override
            public void onSuccess(Message message) {
                RongIMClient.getInstance().sendReadReceiptRequest(message, createOperationCallback("sendReadReceiptRequest"));
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("sendReadReceiptRequest",createCommErrorMsg(errorCode));
            }
        });
    }

    public static void sendReadReceiptResponse(int conversationType, String targetId, String messagesJson) {
        JsonArray messages=toJsonArray(messagesJson);
        ArrayList<Message> list = new ArrayList<>();
        for (int i = 0; i < messages.size(); i += 1) {
            JsonObject map = messages.get(i).getAsJsonObject();
            if (map != null) {
                list.add(toMessage(map));
            }
        }
        RongIMClient.getInstance().sendReadReceiptResponse(
                ConversationType.setValue(conversationType), targetId, list, createOperationCallback("sendReadReceiptResponse"));
    }

    public static void syncConversationReadStatus(int conversationType, String targetId, double timestamp) {
        RongIMClient.getInstance().syncConversationReadStatus(
                ConversationType.setValue(conversationType), targetId, (long) timestamp, createOperationCallback("syncConversationReadStatus"));
    }

    public static void sendTypingStatus(int conversationType, String targetId, String typingStatus) {
        RongIMClient.getInstance().sendTypingStatus(ConversationType.setValue(conversationType), targetId, typingStatus);
    }

    public static int getRealTimeLocation(int conversationType, String targetId) {
        RealTimeLocationErrorCode code = RongIMClient.getInstance().getRealTimeLocation(
                ConversationType.setValue(conversationType), targetId);
        return code.getValue();
    }

    public static void startRealTimeLocation(int conversationType, String targetId) {
        RealTimeLocationErrorCode code = RongIMClient.getInstance().startRealTimeLocation(
                ConversationType.setValue(conversationType), targetId);
        JsonObject msg=new JsonObject();
        if(code==RealTimeLocationErrorCode.RC_REAL_TIME_LOCATION_SUCCESS){
            msg.addProperty(RC_STATUS,RC_SUCCESS);
        }else{
            msg.addProperty(RC_STATUS,RC_ERROR);
            msg.addProperty("errorCode",code.getValue());
        }
        sendCommCallbackMessage("startRealTimeLocation",msg);
    }

    public static void joinRealTimeLocation(int conversationType, String targetId) {
        RealTimeLocationErrorCode code = RongIMClient.getInstance().joinRealTimeLocation(
                ConversationType.setValue(conversationType), targetId);
        JsonObject msg=new JsonObject();
        if(code==RealTimeLocationErrorCode.RC_REAL_TIME_LOCATION_SUCCESS){
            msg.addProperty(RC_STATUS,RC_SUCCESS);
        }else{
            msg.addProperty(RC_STATUS,RC_ERROR);
            msg.addProperty("errorCode",code.getValue());
        }
        sendCommCallbackMessage("joinRealTimeLocation",msg);
    }

    public static void quitRealTimeLocation(int conversationType, String targetId) {
        try {
            RongIMClient.getInstance().quitRealTimeLocation(
                    ConversationType.setValue(conversationType), targetId);
            sendCommCallbackMessage("quitRealTimeLocation",createJsonObject(RC_STATUS,RC_SUCCESS));
        } catch (Exception e) {
            sendCommCallbackMessage("quitRealTimeLocation",createJsonObject(RC_STATUS,RC_ERROR));
        }
    }

    public static void getRealTimeLocationParticipants(int conversationType, String targetId) {
        List<String> list = RongIMClient.getInstance().getRealTimeLocationParticipants(
                ConversationType.setValue(conversationType), targetId);
        JsonArray array = new JsonArray();
        if (list != null) {
            for (String item : list) {
                array.add(item);
            }
        }

        JsonObject msg=new JsonObject();
        msg.addProperty(RC_STATUS,RC_SUCCESS);
        msg.add("ids",array);
        sendCommCallbackMessage("getRealTimeLocationParticipants",msg);


    }

    public static void getRealTimeLocationCurrentState(int conversationType, String targetId) {
        RealTimeLocationStatus status = RongIMClient.getInstance().getRealTimeLocationCurrentState(
                ConversationType.setValue(conversationType), targetId);
        JsonObject msg=new JsonObject();
        msg.addProperty("status",status.getValue());

        sendCommCallbackMessage("getRealTimeLocationCurrentState",msg);
    }

    public static void addRealTimeLocationListener(final int conversationType, final String targetId){
        RongIMClient.getInstance().addRealTimeLocationListener(ConversationType.setValue(conversationType) , targetId, new RealTimeLocationListener() {
            @Override
            public void onStatusChange(RealTimeLocationStatus realTimeLocationStatus) {
                JsonObject msg=createJsonObject("event","onStatusChange");
                msg.addProperty("status",realTimeLocationStatus.getValue());
                sendCommCallbackMessage("addRealTimeLocationListener",msg);
            }

            @Override
            public void onReceiveLocation(double v, double v1, String s) {
                JsonObject msg=createJsonObject("event","onReceiveLocation");
                msg.addProperty("latitude",v);
                msg.addProperty("longitude",v1);
                msg.addProperty("userId",s);
                sendCommCallbackMessage("addRealTimeLocationListener",msg);
            }

            @Override
            public void onReceiveLocationWithType(double v, double v1, RealTimeLocationType realTimeLocationType, String s) {
                JsonObject msg=createJsonObject("event","onReceiveLocationWithType");
                msg.addProperty("latitude",v);
                msg.addProperty("longitude",v1);
                msg.addProperty("userId",s);
                msg.addProperty("type",realTimeLocationType.getValue());
                sendCommCallbackMessage("addRealTimeLocationListener",msg);
            }

            @Override
            public void onParticipantsJoin(String s) {
                JsonObject msg=createJsonObject("event","onParticipantsJoin");
                msg.addProperty("userId",s);
                sendCommCallbackMessage("addRealTimeLocationListener",msg);
            }

            @Override
            public void onParticipantsQuit(String s) {
                JsonObject msg=createJsonObject("event","onParticipantsQuit");
                msg.addProperty("userId",s);
                sendCommCallbackMessage("addRealTimeLocationListener",msg);
            }

            @Override
            public void onError(RealTimeLocationErrorCode realTimeLocationErrorCode) {
                JsonObject msg=createJsonObject("event","onError");
                msg.addProperty("errorCode",realTimeLocationErrorCode.getValue());
                sendCommCallbackMessage("addRealTimeLocationListener",msg);
            }
        });
    }

    public static void removeRealTimeLocationObserver(final int conversationType, final String targetId){
        RongIMClient.getInstance().removeRealTimeLocationObserver(ConversationType.setValue(conversationType),targetId);
    }

    public static void getOfflineMessageDuration() {
        RongIMClient.getInstance().getOfflineMessageDuration(new ResultCallback<String>() {
            @Override
            public void onSuccess(String s) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                msg.addProperty("duration ",s);
                sendCommCallbackMessage("getOfflineMessageDuration",msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("getOfflineMessageDuration",createCommErrorMsg(errorCode));
            }
        });
    }

    public static void setOfflineMessageDuration(int duration) {
        RongIMClient.getInstance().setOfflineMessageDuration(duration, new ResultCallback<Long>() {
            @Override
            public void onSuccess(Long n) {
                JsonObject msg=createJsonObject(RC_STATUS,RC_SUCCESS);
                msg.addProperty("duration ",n);
                sendCommCallbackMessage("setOfflineMessageDuration",msg);
            }

            @Override
            public void onError(ErrorCode errorCode) {
                sendCommCallbackMessage("setOfflineMessageDuration",createCommErrorMsg(errorCode));
            }
        });
    }

}

package cn.rongcloud.imlib.unity;

import android.content.Context;
import io.rong.push.PushType;
import io.rong.push.notification.PushMessageReceiver;
import io.rong.push.notification.PushNotificationMessage;
import static cn.rongcloud.imlib.unity.Convert.*;
import static cn.rongcloud.imlib.unity.Utils.*;

public class RCPushReceiver extends PushMessageReceiver{
    @Override
    public boolean onNotificationMessageArrived(Context context, PushType pushType, PushNotificationMessage message) {
        sendCommCallbackMessage("pushmessage-arrived",Convert.toJson(message,pushType));

        return false;
    }

    @Override
    public boolean onNotificationMessageClicked(Context context, PushType pushType, PushNotificationMessage message) {
        sendCommCallbackMessage("pushmessage-clicked",Convert.toJson(message,pushType));
        return false;
    }
}

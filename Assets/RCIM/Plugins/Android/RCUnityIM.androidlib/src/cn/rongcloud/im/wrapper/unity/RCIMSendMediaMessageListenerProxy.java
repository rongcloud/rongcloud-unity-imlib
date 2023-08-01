package cn.rongcloud.im.wrapper.unity;

import cn.rongcloud.im.wrapper.listener.IRCIMIWSendMediaMessageListener;
import cn.rongcloud.im.wrapper.listener.RCIMIWSendMediaMessageListener;
import cn.rongcloud.im.wrapper.messages.RCIMIWMediaMessage;

public class RCIMSendMediaMessageListenerProxy extends RCIMIWSendMediaMessageListener {
  IRCIMIWSendMediaMessageListener cb;

  public RCIMSendMediaMessageListenerProxy(IRCIMIWSendMediaMessageListener listener) {
    this.cb = listener;
  }

  public void onMediaMessageSaved(RCIMIWMediaMessage message) {
    cb.onMediaMessageSaved(message);
  }

  public void onMediaMessageSending(RCIMIWMediaMessage message, int progress) {
    cb.onMediaMessageSending(message, progress);
  }

  public void onSendingMediaMessageCanceled(RCIMIWMediaMessage message) {
    cb.onSendingMediaMessageCanceled(message);
  }

  public void onMediaMessageSent(int code, RCIMIWMediaMessage message) {
    cb.onMediaMessageSent(code, message);
  }
}

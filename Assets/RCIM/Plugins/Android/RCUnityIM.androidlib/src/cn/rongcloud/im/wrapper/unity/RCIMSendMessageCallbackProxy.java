package cn.rongcloud.im.wrapper.unity;

import cn.rongcloud.im.wrapper.callback.IRCIMIWSendMessageCallback;
import cn.rongcloud.im.wrapper.callback.RCIMIWSendMessageCallback;
import cn.rongcloud.im.wrapper.messages.RCIMIWMessage;

public class RCIMSendMessageCallbackProxy extends RCIMIWSendMessageCallback {
  IRCIMIWSendMessageCallback cb;

  public RCIMSendMessageCallbackProxy(IRCIMIWSendMessageCallback listener) {
    this.cb = listener;
  }

  public void onMessageSaved(RCIMIWMessage message) {
    cb.onMessageSaved(message);
  }

  public void onMessageSent(int code, RCIMIWMessage message) {
    cb.onMessageSent(code, message);
  }
}

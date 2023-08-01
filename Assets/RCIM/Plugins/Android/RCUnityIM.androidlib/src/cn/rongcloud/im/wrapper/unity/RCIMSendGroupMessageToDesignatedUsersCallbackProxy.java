package cn.rongcloud.im.wrapper.unity;

import cn.rongcloud.im.wrapper.callback.IRCIMIWSendGroupMessageToDesignatedUsersCallback;
import cn.rongcloud.im.wrapper.callback.RCIMIWSendGroupMessageToDesignatedUsersCallback;
import cn.rongcloud.im.wrapper.messages.RCIMIWMessage;

public class RCIMSendGroupMessageToDesignatedUsersCallbackProxy
    extends RCIMIWSendGroupMessageToDesignatedUsersCallback {
  IRCIMIWSendGroupMessageToDesignatedUsersCallback cb;

  public RCIMSendGroupMessageToDesignatedUsersCallbackProxy(
      IRCIMIWSendGroupMessageToDesignatedUsersCallback listener) {
    this.cb = listener;
  }

  public void onMessageSaved(RCIMIWMessage message) {
    cb.onMessageSaved(message);
  }

  public void onMessageSent(int code, RCIMIWMessage message) {
    cb.onMessageSent(code, message);
  }
}

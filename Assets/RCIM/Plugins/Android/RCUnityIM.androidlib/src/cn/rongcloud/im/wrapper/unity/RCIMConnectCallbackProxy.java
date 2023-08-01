package cn.rongcloud.im.wrapper.unity;

import cn.rongcloud.im.wrapper.callback.IRCIMIWConnectCallback;
import cn.rongcloud.im.wrapper.callback.RCIMIWConnectCallback;

public class RCIMConnectCallbackProxy extends RCIMIWConnectCallback {
  IRCIMIWConnectCallback cb;

  public RCIMConnectCallbackProxy(IRCIMIWConnectCallback listener) {
    this.cb = listener;
  }

  public void onDatabaseOpened(int code) {
    cb.onDatabaseOpened(code);
  }

  public void onConnected(int code, String userId) {
    cb.onConnected(code, userId);
  }
}

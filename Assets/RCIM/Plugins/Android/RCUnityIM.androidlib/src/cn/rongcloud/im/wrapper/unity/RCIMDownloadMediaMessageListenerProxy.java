package cn.rongcloud.im.wrapper.unity;

import cn.rongcloud.im.wrapper.listener.IRCIMIWDownloadMediaMessageListener;
import cn.rongcloud.im.wrapper.listener.RCIMIWDownloadMediaMessageListener;
import cn.rongcloud.im.wrapper.messages.RCIMIWMediaMessage;

public class RCIMDownloadMediaMessageListenerProxy extends RCIMIWDownloadMediaMessageListener {
  IRCIMIWDownloadMediaMessageListener cb;

  public RCIMDownloadMediaMessageListenerProxy(IRCIMIWDownloadMediaMessageListener listener) {
    this.cb = listener;
  }

  public void onMediaMessageDownloading(RCIMIWMediaMessage message, int progress) {
    cb.onMediaMessageDownloading(message, progress);
  }

  public void onDownloadingMediaMessageCanceled(RCIMIWMediaMessage message) {
    cb.onDownloadingMediaMessageCanceled(message);
  }

  public void onMediaMessageDownloaded(int code, RCIMIWMediaMessage message) {
    cb.onMediaMessageDownloaded(code, message);
  }
}

using System.Collections.Generic;
using cn_rongcloud_im_unity;

public class TagConversationContext
{
    public enum EditMode
    {
        SelectConversationForAdd,
        SelectConversationForRemove,
        ShowConversationsInTag,
    }

    public RCTagInfo CurrentTag { get; set; }
    public IList<RCConversation> ConversationSourceForTag { get; set; }
    public EditMode Mode { get; set; }
}
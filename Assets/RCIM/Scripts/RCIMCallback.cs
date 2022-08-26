//
//  Copyright © 2021 RongCloud. All rights reserved.
//

using System.Collections.Generic;

namespace cn_rongcloud_im_unity
{
    public delegate void OperationCallback(RCErrorCode errorCode);
    public delegate void OperationCallbackWithResult<T>(RCErrorCode errorCode, T result);
}
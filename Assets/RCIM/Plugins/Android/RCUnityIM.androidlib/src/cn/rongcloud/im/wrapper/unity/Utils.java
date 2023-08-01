//<#CLASS_NAME=JavaUtils#>
package cn.rongcloud.im.wrapper.unity;

public class Utils {
    public static boolean isInstanceOf(Object obj, String javaClass) {
        // 判断 obj 是否为 javaClass 的子类
        if (obj == null)
            return false;
        try {
            Class<?> clazz = Class.forName(javaClass);
            return clazz.isInstance(obj);
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
            return false;
        }
    }
}
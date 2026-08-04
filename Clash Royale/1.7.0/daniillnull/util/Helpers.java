package daniillnull.util;

public class Helpers {
    public static byte[] concat(byte[] one, byte[] two) {
        byte[] all = new byte[one.length + two.length];
        System.arraycopy(one, 0, all, 0, one.length);
        System.arraycopy(two, 0, all, one.length, two.length);
        return all;
    }

    public static byte[] concat(byte[] one, byte[] two, byte[] three) {
        byte[] all = new byte[one.length + two.length + three.length];
        System.arraycopy(one, 0, all, 0, one.length);
        System.arraycopy(two, 0, all, one.length, two.length);
        System.arraycopy(three, 0, all, one.length + two.length, three.length);
        return all;
    }

    public static byte[] parseHexBinary(String s) {
        int len = s.length();
        if (len % 2 != 0) {
            throw new IllegalArgumentException("Hex string must have even length");
        }
        byte[] data = new byte[len / 2];
        for (int i = 0; i < len; i += 2) {
            int hi = Character.digit(s.charAt(i), 16);
            int lo = Character.digit(s.charAt(i + 1), 16);
            if (hi == -1 || lo == -1) {
                throw new IllegalArgumentException("Invalid hex character: " + s.charAt(i) + s.charAt(i + 1));
            }
            data[i / 2] = (byte) ((hi << 4) + lo);
        }
        return data;
    }
}
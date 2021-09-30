/*
 * Tuscia klase (sablonas), kuria galima panaudoti kuriant savo metodus
 * Tiesiog nusikopijuokite sio failo turini i savo projekta ir pridekite
 * savo sukurtus metodus
 */

package com.mycompany.sauga_lab3;

import java.math.BigInteger;
import java.security.Key;
import java.security.KeyFactory;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.Provider;
import java.security.SecureRandom;
import java.security.Security;
import java.util.Iterator;
import javax.crypto.Cipher;
import javax.crypto.Mac;
import javax.crypto.SecretKey;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;
import org.bouncycastle.util.encoders.Hex;
import org.bouncycastle.jce.interfaces.ElGamalPrivateKey;
import org.bouncycastle.jce.interfaces.ElGamalPublicKey;
import org.bouncycastle.jce.spec.ElGamalParameterSpec;
import org.bouncycastle.jce.spec.ElGamalPrivateKeySpec;
import org.bouncycastle.jce.spec.ElGamalPublicKeySpec;

/**
 *
 * @author nerijus
 */
public class Main
{
    private static final String	digits = "0123456789ABCDEF";
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) throws Exception
    {
        // Savo uzduotis realizuokite kaip klases Main metodus
        // ir juos iskvieskite is sio metodo, kaip pavyzdziui:

        //doListBCCapabilities();
        //doSimplePolicyTest();

        //Uzduotis2();
        //Uzduotis21();
//        Uzduotis22();
        //Uzduotis23();
//        
        //doDecryptSeed();
        //doDecryptSeed2();
        //doDecryptSeed3();
        //doDecryptSeed4();
//        
        //doMD5Check();
        //doTigerCheck();
        //doRipeMD320Check();
        //doMD5CheckMOD();
//        doRipeMD320CheckMOD();
//        
        //doSHA1HMACCheck();
        //doMD2HMACCheck();
        //doSHA512HMACCheck();
        //doSHA1HMACCheckMOD();
        //doMD2HMACCheckMOD();
        //doSHA512HMACCheckMOD();
//        
        doElGamalDecrypt();
        doElGamalDecryptMOD();
    }

 /**
 * Test to make sure the unrestricted policy files are installed.
 */
    public static void doSimplePolicyTest() throws Exception{
        byte[] data = { 0x10, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };

        // create a 64 bit secret key from raw bytes

        SecretKey key64 = new SecretKeySpec(new byte[] { 0x00, 0x01, 0x02,
                0x03, 0x04, 0x05, 0x06, 0x07 }, "Blowfish");

        // create a cipher and attempt to encrypt the data block with our key

        Cipher c = Cipher.getInstance("Blowfish/ECB/NoPadding");

        c.init(Cipher.ENCRYPT_MODE, key64);
        c.doFinal(data);
        System.out.println("64 bit test: passed");

        // create a 128 bit secret key from raw bytes

        SecretKey key128 = new SecretKeySpec(new byte[] { 0x00, 0x01, 0x02,
                0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c,
                0x0d, 0x0e, 0x0f}, "Blowfish");

        // now try encrypting with the larger key

        c.init(Cipher.ENCRYPT_MODE, key128);
        System.out.println("128 bit test: passed ["+ ((byte[]) c.doFinal(data)).toString()+"]");

        // create a 192 bit secret key from raw bytes

        SecretKey key192 = new SecretKeySpec(new byte[] { 0x00, 0x01, 0x02,
                0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c,
                0x0d, 0x0e, 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16,
                0x17 }, "Blowfish");

        // now try encrypting with the larger key

        c.init(Cipher.ENCRYPT_MODE, key192);
        c.doFinal(data);
        System.out.println("192 bit test: passed");

        System.out.println("Tests completed");
    }

    /**
      * List the available capabilities for ciphers, key agreement, macs, message
      * digests, signatures and other objects in the BC provider.
    */
    public static void doListBCCapabilities() throws Exception {
        Provider	provider = Security.getProvider("BC");
        Iterator        it = provider.keySet().iterator();
        
        while (it.hasNext())
        {
            String	entry = (String)it.next();           
            // this indicates the entry refers to another entry
            if (entry.startsWith("Alg.Alias."))
            {
                entry = entry.substring("Alg.Alias.".length());
            }
            String  factoryClass = entry.substring(0, entry.indexOf('.'));
            String  name = entry.substring(factoryClass.length() + 1);

            System.out.println(factoryClass + ": " + name);
        }
    }
    
    /**
     * Du pagalbiniai metodai skirti "graziai" atvaizduoti baitu masyvus
     */
    public static String toHex(byte[] data, int length){
        StringBuffer	buf = new StringBuffer();
        for (int i = 0; i != length; i++)
        {
            int	v = data[i] & 0xff;

            buf.append(digits.charAt(v >> 4));
            buf.append(digits.charAt(v & 0xf));

            if (((i+1) % 8 == 0) && (i>0)) buf.append(" ");

        }
        return buf.toString();
    }

    public static String toHex(byte[] data){
        return toHex(data, data.length);
    }
    
    public static void Uzduotis2() throws Exception {
        byte[] tekstograma = Hex.decode("719AEAA97C5A673B 5C4B61E822F5E5F5 3280868F660CA282 2488E8BDCA6AC6EB");
        byte[] raktas = Hex.decode("0001020304050607 08090A0B0C0D0E0F");
        
        SecretKeySpec key = new SecretKeySpec(raktas, 0, raktas.length, "AES");
        Cipher cipher = Cipher.getInstance("AES/ECB/NoPadding", "BC");
        cipher.init(Cipher.ENCRYPT_MODE, key);
        
        byte[] sifrograma = new byte[cipher.getOutputSize(tekstograma.length)];
        int ctLength = cipher.update(tekstograma, 0, tekstograma.length, sifrograma, 0);
        ctLength += cipher.doFinal(sifrograma, ctLength);
        
        System.out.println("Užšifruota tekstograma : " + toHex(sifrograma, ctLength) + " bytes: " + ctLength);
        System.out.println("Naudotas raktas : " + toHex(raktas));
        
        cipher.init(Cipher.DECRYPT_MODE, key);
        byte[] plainText = new byte[cipher.getOutputSize(ctLength)];
        int ptLength = cipher.update(sifrograma, 0, sifrograma.length, plainText, 0);
        ptLength += cipher.doFinal(plainText, ptLength);
        System.out.println("Vėl iššifruota tekstograma : " + toHex(plainText, ptLength) + " bytes: " + ptLength);
    }
    
    public static void Uzduotis21() throws Exception {
        byte[] tekstograma = Hex.decode("629AEAA97C5A673B 5C4B61E822F5E5F5 3280868F660CA282 2488E8BDCA6AC6EB");
        byte[] raktas = Hex.decode("0001020304050607 08090A0B0C0D0E0F");
        
        SecretKeySpec key = new SecretKeySpec(raktas, 0, raktas.length, "AES");
        Cipher cipher = Cipher.getInstance("AES/ECB/NoPadding", "BC");
        cipher.init(Cipher.ENCRYPT_MODE, key);
        
        byte[] sifrograma = new byte[cipher.getOutputSize(tekstograma.length)];
        int ctLength = cipher.update(tekstograma, 0, tekstograma.length, sifrograma, 0);
        ctLength += cipher.doFinal(sifrograma, ctLength);
        
        System.out.println("Užšifruota tekstograma : " + toHex(sifrograma, ctLength) + " bytes: " + ctLength);
        System.out.println("Naudotas raktas : " + toHex(raktas));
        
        cipher.init(Cipher.DECRYPT_MODE, key);
        byte[] plainText = new byte[cipher.getOutputSize(ctLength)];
        int ptLength = cipher.update(sifrograma, 0, sifrograma.length, plainText, 0);
        ptLength += cipher.doFinal(plainText, ptLength);
        System.out.println("Vėl iššifruota tekstograma : " + toHex(plainText, ptLength) + " bytes: " + ptLength);
    }
    
    public static void Uzduotis22() throws Exception {
        byte[] tekstograma = Hex.decode("619AEAA97C5A673B 5C4B61E822F5E5F5 3280868F660CA282 2488E8BDCA6AC6EB 1");
        byte[] raktas = Hex.decode("0001020304050607 08090A0B0C0D0E0F");
        
        SecretKeySpec key = new SecretKeySpec(raktas, 0, raktas.length, "AES");
        Cipher cipher = Cipher.getInstance("AES/ECB/NoPadding", "BC");
        cipher.init(Cipher.ENCRYPT_MODE, key);
        
        byte[] sifrograma = new byte[cipher.getOutputSize(tekstograma.length)];
        int ctLength = cipher.update(tekstograma, 0, tekstograma.length, sifrograma, 0);
        ctLength += cipher.doFinal(sifrograma, ctLength);
        
        System.out.println("Užšifruota tekstograma : " + toHex(sifrograma, ctLength) + " bytes: " + ctLength);
        System.out.println("Naudotas raktas : " + toHex(raktas));
        
        cipher.init(Cipher.DECRYPT_MODE, key);
        byte[] plainText = new byte[cipher.getOutputSize(ctLength)];
        int ptLength = cipher.update(sifrograma, 0, sifrograma.length, plainText, 0);
        ptLength += cipher.doFinal(plainText, ptLength);
        System.out.println("Vėl iššifruota tekstograma : " + toHex(plainText, ptLength) + " bytes: " + ptLength);
    }
    
    public static void Uzduotis23() throws Exception {
        
        byte[] raktas = Hex.decode("0001020304050607 08090A0B0C0D0E0F");
        
        SecretKeySpec key = new SecretKeySpec(raktas, 0, raktas.length, "AES");
        Cipher cipher = Cipher.getInstance("AES/ECB/NoPadding", "BC");
        
        byte[] sifrograma = Hex.decode("BCDC000000000000 0000000000000000 0000000000000000 000000000000ACDC");
        
        System.out.println("Užšifruota tekstograma su pakeistu pirmu bitu: " + toHex(sifrograma, sifrograma.length) + " bytes: " + sifrograma.length);
        System.out.println("Naudotas raktas : " + toHex(raktas));
        
        cipher.init(Cipher.DECRYPT_MODE, key);
        byte[] plainText = new byte[cipher.getOutputSize(sifrograma.length)];
        int ptLength = cipher.update(sifrograma, 0, sifrograma.length, plainText, 0);
        ptLength += cipher.doFinal(plainText, ptLength);
        System.out.println("Vėl iššifruota tekstograma : " + toHex(plainText, ptLength) + " bytes: " + ptLength);
    }

    public static void doDecryptSeed() throws Exception {
        byte[]  keyBytes = Hex.decode ("6665566666655666 3331133333311333");
        byte[]  input = Hex.decode ("5CADE7A3D99AAEC2 A016DA4A307C5DDA 3DF5C1A1D1A0EF26 DA91D7E94D7292B7");
        byte[]	ivBytes = Hex.decode ("0706050403020100 08090A0B0C0D0E0F");

        System.out.println("Duotoji šifrograma : " + toHex(input));
        SecretKeySpec   key = new SecretKeySpec(keyBytes, 0, 16, "seed");
        // IV turi buti lygiai tiek baitu, koks yra bloko ilgis
        IvParameterSpec ivSpec = new IvParameterSpec(ivBytes, 0, 16);
        Cipher          cipher = Cipher.getInstance("seed/CBC/PKCS7Padding", "BC");

        cipher.init(Cipher.DECRYPT_MODE, key, ivSpec);
        byte[] plainText = new byte[cipher.getOutputSize(input.length)];

        int ptLength = cipher.update(input, 0, input.length, plainText, 0);
        ptLength += cipher.doFinal(plainText, ptLength);
        System.out.println("Seed iššifruota tekstograma : " + toHex(plainText, ptLength) + " bytes: " + ptLength);
        byte[] raktas = key.getEncoded();
        System.out.println("Naudotas raktas : " + toHex(raktas));
        System.out.println("Naudotas IV : " + toHex(ivSpec.getIV()));

        //Patikrinimas
        cipher.init(Cipher.ENCRYPT_MODE, key, ivSpec);
        byte[] cipherText = new byte[cipher.getOutputSize(ptLength)];

        int ctLength = cipher.update(plainText, 0, ptLength, cipherText, 0);
        ctLength += cipher.doFinal(cipherText, ctLength);

        System.out.println("Vėl užšifruota tekstograma : " + toHex(cipherText, ctLength) + " bytes: " + ctLength);
    }
    
    public static void doDecryptSeed2() throws Exception {
        byte[]  keyBytes = Hex.decode ("6665566666655666 3331133333311333");
        byte[]	ivBytes = Hex.decode ("0706050403020100 08090A0B0C0D0E0F");
        
        SecretKeySpec   key = new SecretKeySpec(keyBytes, 0, 16, "seed");
        // IV turi buti lygiai tiek baitu, koks yra bloko ilgis
        IvParameterSpec ivSpec = new IvParameterSpec(ivBytes, 0, 16);
        Cipher          cipher = Cipher.getInstance("seed/CBC/PKCS7Padding", "BC");
        byte[] plainText = Hex.decode ("BABE000000000000 1011121300000000 00DAF011");

        System.out.println("Seed iššifruota tekstograma su pakeistu vienu bitu: " + toHex(plainText, plainText.length) + " bytes: " + plainText.length);
        byte[] raktas = key.getEncoded();
        System.out.println("Naudotas raktas : " + toHex(raktas));
        System.out.println("Naudotas IV : " + toHex(ivSpec.getIV()));

        //Patikrinimas
        cipher.init(Cipher.ENCRYPT_MODE, key, ivSpec);
        byte[] cipherText = new byte[cipher.getOutputSize(plainText.length)];

        int ctLength = cipher.update(plainText, 0, plainText.length, cipherText, 0);
        ctLength += cipher.doFinal(cipherText, ctLength);

        System.out.println("Vėl užšifruota tekstograma : " + toHex(cipherText, ctLength) + " bytes: " + ctLength);
    }
    
    public static void doDecryptSeed3() throws Exception {
        byte[]  keyBytes = Hex.decode ("6665566666655666 3331133333311333");
        byte[]  input = Hex.decode ("6DADE7A3D99AAEC2 A016DA4A307C5DDA 3DF5C1A1D1A0EF26 DA91D7E94D7292B7");
        byte[]	ivBytes = Hex.decode ("0706050403020100 08090A0B0C0D0E0F");

        System.out.println("Duotoji šifrograma su pakeistu vienu bitu : " + toHex(input));
        SecretKeySpec   key = new SecretKeySpec(keyBytes, 0, 16, "seed");
        // IV turi buti lygiai tiek baitu, koks yra bloko ilgis
        IvParameterSpec ivSpec = new IvParameterSpec(ivBytes, 0, 16);
        Cipher          cipher = Cipher.getInstance("seed/CBC/PKCS7Padding", "BC");

        cipher.init(Cipher.DECRYPT_MODE, key, ivSpec);
        byte[] plainText = new byte[cipher.getOutputSize(input.length)];

        int ptLength = cipher.update(input, 0, input.length, plainText, 0);
        ptLength += cipher.doFinal(plainText, ptLength);
        System.out.println("Seed iššifruota tekstograma : " + toHex(plainText, ptLength) + " bytes: " + ptLength);
        byte[] raktas = key.getEncoded();
        System.out.println("Naudotas raktas : " + toHex(raktas));
        System.out.println("Naudotas IV : " + toHex(ivSpec.getIV()));

        //Patikrinimas
        cipher.init(Cipher.ENCRYPT_MODE, key, ivSpec);
        byte[] cipherText = new byte[cipher.getOutputSize(ptLength)];

        int ctLength = cipher.update(plainText, 0, ptLength, cipherText, 0);
        ctLength += cipher.doFinal(cipherText, ctLength);

        System.out.println("Vėl užšifruota tekstograma : " + toHex(cipherText, ctLength) + " bytes: " + ctLength);
    }
    
    public static void doDecryptSeed4() throws Exception {
        byte[]  keyBytes = Hex.decode ("6665566666655666 3331133333311333");
        byte[]  input = Hex.decode ("5CADE7A3D99AAEC2 A016DA4A307C5DDA 3DF5C1A1D1A0EF26 DA91D7E94D7292B7");
        byte[]	ivBytes = Hex.decode ("1806050403020100 08090A0B0C0D0E0F");

        System.out.println("Duotoji šifrograma : " + toHex(input));
        SecretKeySpec   key = new SecretKeySpec(keyBytes, 0, 16, "seed");
        // IV turi buti lygiai tiek baitu, koks yra bloko ilgis
        IvParameterSpec ivSpec = new IvParameterSpec(ivBytes, 0, 16);
        Cipher          cipher = Cipher.getInstance("seed/CBC/PKCS7Padding", "BC");

        cipher.init(Cipher.DECRYPT_MODE, key, ivSpec);
        byte[] plainText = new byte[cipher.getOutputSize(input.length)];

        int ptLength = cipher.update(input, 0, input.length, plainText, 0);
        ptLength += cipher.doFinal(plainText, ptLength);
        System.out.println("Seed iššifruota tekstograma : " + toHex(plainText, ptLength) + " bytes: " + ptLength);
        byte[] raktas = key.getEncoded();
        System.out.println("Naudotas raktas : " + toHex(raktas));
        System.out.println("Naudotas IV kuriame pakeistas vienas bitas: " + toHex(ivSpec.getIV()));

        //Patikrinimas
        cipher.init(Cipher.ENCRYPT_MODE, key, ivSpec);
        byte[] cipherText = new byte[cipher.getOutputSize(ptLength)];

        int ctLength = cipher.update(plainText, 0, ptLength, cipherText, 0);
        ctLength += cipher.doFinal(cipherText, ctLength);

        System.out.println("Vėl užšifruota tekstograma : " + toHex(cipherText, ctLength) + " bytes: " + ctLength);
    }
    
    public static void doMD5Check() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("BABCE00004050607 08090A0B0C0D");
        byte[]  hashBytes = Hex.decode ("4119F924ACD0A299 C33E66A3183D70FC");

        System.out.println("Tekstograma : " + toHex(inputBytes));
        System.out.println("MD5 santrauka : " + toHex(hashBytes));

        MessageDigest hash = MessageDigest.getInstance("MD5", "BC");

        hash.update(inputBytes, 0, inputBytes.length);
        byte[] inputHash = new byte[hash.getDigestLength()];
        inputHash = hash.digest();

        System.out.println("Apskaičiuota santrauka : " + toHex(inputHash));

        ok = MessageDigest.isEqual(inputHash, hashBytes);
        System.out.println("Tekstogra nepakeista? : " + ok);
    }
    
    public static void doTigerCheck() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("BAD0ACE004050607 08090A0B0C00");
        byte[]  hashBytes = Hex.decode ("8D840257D657B778 2E8D65A5A6BEC9B8 3C705331701B");

        System.out.println("Tekstograma : " + toHex(inputBytes));
        System.out.println("Tiger santrauka : " + toHex(hashBytes));

        MessageDigest hash = MessageDigest.getInstance("Tiger", "BC");

        hash.update(inputBytes, 0, inputBytes.length);
        byte[] inputHash = new byte[hash.getDigestLength()];
        inputHash = hash.digest();

        System.out.println("Apskaičiuota santrauka : " + toHex(inputHash));

        ok = MessageDigest.isEqual(inputHash, hashBytes);
        System.out.println("Tekstogra nepakeista? : " + ok);
    }
    
    public static void doRipeMD320Check() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("ABBA000004050607 08090A0B0C0D");
        byte[]  hashBytes = Hex.decode ("25399BCEC86662AA 1379862A91CB79E7 D50C1050CCEC2726 C9B086F44735B134 FB44BB8BA99B326D");

        System.out.println("Tekstograma : " + toHex(inputBytes));
        System.out.println("RipeMD320 santrauka : " + toHex(hashBytes));

        MessageDigest hash = MessageDigest.getInstance("RipeMD320", "BC");

        hash.update(inputBytes, 0, inputBytes.length);
        byte[] inputHash = new byte[hash.getDigestLength()];
        inputHash = hash.digest();

        System.out.println("Apskaičiuota santrauka : " + toHex(inputHash));

        ok = MessageDigest.isEqual(inputHash, hashBytes);
        System.out.println("Tekstogra nepakeista? : " + ok);
    }
    
    public static void doMD5CheckMOD() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("ABBCE00004050607 08090A0B0C0D");
        byte[]  hashBytes = Hex.decode ("4119F924ACD0A299 C33E66A3183D70FC");

        System.out.println("Tekstograma su pakeistu vienu baitu : " + toHex(inputBytes));
        System.out.println("MD5 santrauka : " + toHex(hashBytes));

        MessageDigest hash = MessageDigest.getInstance("MD5", "BC");

        hash.update(inputBytes, 0, inputBytes.length);
        byte[] inputHash = new byte[hash.getDigestLength()];
        inputHash = hash.digest();

        System.out.println("Apskaičiuota santrauka : " + toHex(inputHash));

        ok = MessageDigest.isEqual(inputHash, hashBytes);
        System.out.println("Tekstogra nepakeista? : " + ok);
    }
    
    public static void doRipeMD320CheckMOD() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("BBBA000004050607 08090A0B0C0D");
        byte[]  hashBytes = Hex.decode ("25399BCEC86662AA 1379862A91CB79E7 D50C1050CCEC2726 C9B086F44735B134 FB44BB8BA99B326D");

        System.out.println("Tekstograma su pakeistu vienu baitu : " + toHex(inputBytes));
        System.out.println("RipeMD320 santrauka : " + toHex(hashBytes));

        MessageDigest hash = MessageDigest.getInstance("RipeMD320", "BC");

        hash.update(inputBytes, 0, inputBytes.length);
        byte[] inputHash = new byte[hash.getDigestLength()];
        inputHash = hash.digest();

        System.out.println("Apskaičiuota santrauka : " + toHex(inputHash));

        ok = MessageDigest.isEqual(inputHash, hashBytes);
        System.out.println("Tekstogra nepakeista? : " + ok);
    }
    
    public static void doSHA1HMACCheck() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("BABCE00000010203 040506070809");
        byte[]  macKeyBytes = Hex.decode ("5172333435363738 393A3B3C");
        byte[]  hmacBytes = Hex.decode ("6332945834FCB38D 6F24CB558FBC5E24 A810799A");


        Mac hMac = Mac.getInstance("Hmac-SHA1", "BC");
        Key hMacKey = new SecretKeySpec(macKeyBytes,"Hmac-SHA1");

        System.out.println("Tekstograma : " + toHex(inputBytes));
        System.out.println("Slaptas raktas : " + toHex(macKeyBytes));
        System.out.println("Pateiktas hmac : " + toHex(hmacBytes));

        hMac.init(hMacKey);
        hMac.update(inputBytes, 0, inputBytes.length);

        byte[] inputMac = new byte[hMac.getMacLength()];
        inputMac = hMac.doFinal();

        System.out.println("Apskaiciuotas hmac : " + toHex(inputMac) + " ilgis " + hMac.getMacLength());

        ok = MessageDigest.isEqual(inputMac, hmacBytes);
        System.out.println("Pranesimas nesuklastotas : " + ok);
    }
    
    public static void doMD2HMACCheck() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("BABA000000010203 0405060708090A0B 0C");
        byte[]  macKeyBytes = Hex.decode ("517233343536");
        byte[]  hmacBytes = Hex.decode ("78F37E497409772E 42453CBF7208DBD6");


        Mac hMac = Mac.getInstance("Hmac-MD2", "BC");
        Key hMacKey = new SecretKeySpec(macKeyBytes,"Hmac-MD2");

        System.out.println("Tekstograma : " + toHex(inputBytes));
        System.out.println("Slaptas raktas : " + toHex(macKeyBytes));
        System.out.println("Pateiktas hmac : " + toHex(hmacBytes));

        hMac.init(hMacKey);
        hMac.update(inputBytes, 0, inputBytes.length);

        byte[] inputMac = new byte[hMac.getMacLength()];
        inputMac = hMac.doFinal();

        System.out.println("Apskaiciuotas hmac : " + toHex(inputMac) + " ilgis " + hMac.getMacLength());

        ok = MessageDigest.isEqual(inputMac, hmacBytes);
        System.out.println("Pranesimas nesuklastotas : " + ok);
    }
    
    public static void doSHA512HMACCheck() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("FACE000000010203 040506070800");
        byte[]  macKeyBytes = Hex.decode ("5172333435363738");
        byte[]  hmacBytes = Hex.decode ("D1CC001ACEB0CD1E 00AD61F9E130FABF EB3D10C0D7068AC9 1C0308CC10A4393C 5D9ABFB7762A0012 C15B23D1C2316712 D02DA19ECC7BFA34 26A1533D62FBEE7F");


        Mac hMac = Mac.getInstance("Hmac-SHA512", "BC");
        Key hMacKey = new SecretKeySpec(macKeyBytes,"Hmac-SHA512");

        System.out.println("Tekstograma : " + toHex(inputBytes));
        System.out.println("Slaptas raktas : " + toHex(macKeyBytes));
        System.out.println("Pateiktas hmac : " + toHex(hmacBytes));

        hMac.init(hMacKey);
        hMac.update(inputBytes, 0, inputBytes.length);

        byte[] inputMac = new byte[hMac.getMacLength()];
        inputMac = hMac.doFinal();

        System.out.println("Apskaiciuotas hmac : " + toHex(inputMac) + " ilgis " + hMac.getMacLength());

        ok = MessageDigest.isEqual(inputMac, hmacBytes);
        System.out.println("Pranesimas nesuklastotas : " + ok);
    }
    
    public static void doSHA1HMACCheckMOD() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("BABCE00000010203 040506070809");
        byte[]  macKeyBytes = Hex.decode ("5172333435363738 393A3B3CC");
        byte[]  hmacBytes = Hex.decode ("6332945834FCB38D 6F24CB558FBC5E24 A810799A");


        Mac hMac = Mac.getInstance("Hmac-SHA1", "BC");
        Key hMacKey = new SecretKeySpec(macKeyBytes,"Hmac-SHA1");

        System.out.println("Tekstograma : " + toHex(inputBytes));
        System.out.println("Slaptas raktas pailgintas vienu bitu: " + toHex(macKeyBytes));
        System.out.println("Pateiktas hmac : " + toHex(hmacBytes));

        hMac.init(hMacKey);
        hMac.update(inputBytes, 0, inputBytes.length);

        byte[] inputMac = new byte[hMac.getMacLength()];
        inputMac = hMac.doFinal();

        System.out.println("Apskaiciuotas hmac : " + toHex(inputMac) + " ilgis " + hMac.getMacLength());

        ok = MessageDigest.isEqual(inputMac, hmacBytes);
        System.out.println("Pranesimas nesuklastotas : " + ok);
    }
    
    public static void doMD2HMACCheckMOD() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("BABA000000010203 0405060708090A0B 0C");
        byte[]  macKeyBytes = Hex.decode ("5172333435366");
        byte[]  hmacBytes = Hex.decode ("78F37E497409772E 42453CBF7208DBD6");


        Mac hMac = Mac.getInstance("Hmac-MD2", "BC");
        Key hMacKey = new SecretKeySpec(macKeyBytes,"Hmac-MD2");

        System.out.println("Tekstograma : " + toHex(inputBytes));
        System.out.println("Slaptas raktas : " + toHex(macKeyBytes));
        System.out.println("Pateiktas hmac : " + toHex(hmacBytes));

        hMac.init(hMacKey);
        hMac.update(inputBytes, 0, inputBytes.length);

        byte[] inputMac = new byte[hMac.getMacLength()];
        inputMac = hMac.doFinal();

        System.out.println("Apskaiciuotas hmac : " + toHex(inputMac) + " ilgis " + hMac.getMacLength());

        ok = MessageDigest.isEqual(inputMac, hmacBytes);
        System.out.println("Pranesimas nesuklastotas : " + ok);
    }
    
    public static void doSHA512HMACCheckMOD() throws Exception {
        boolean ok = false;
        byte[]  inputBytes = Hex.decode ("FACE000000010203 040506070800");
        byte[]  macKeyBytes = Hex.decode ("51723334353637388");
        byte[]  hmacBytes = Hex.decode ("D1CC001ACEB0CD1E 00AD61F9E130FABF EB3D10C0D7068AC9 1C0308CC10A4393C 5D9ABFB7762A0012 C15B23D1C2316712 D02DA19ECC7BFA34 26A1533D62FBEE7F");


        Mac hMac = Mac.getInstance("Hmac-SHA512", "BC");
        Key hMacKey = new SecretKeySpec(macKeyBytes,"Hmac-SHA512");

        System.out.println("Tekstograma : " + toHex(inputBytes));
        System.out.println("Slaptas raktas : " + toHex(macKeyBytes));
        System.out.println("Pateiktas hmac : " + toHex(hmacBytes));

        hMac.init(hMacKey);
        hMac.update(inputBytes, 0, inputBytes.length);

        byte[] inputMac = new byte[hMac.getMacLength()];
        inputMac = hMac.doFinal();

        System.out.println("Apskaiciuotas hmac : " + toHex(inputMac) + " ilgis " + hMac.getMacLength());

        ok = MessageDigest.isEqual(inputMac, hmacBytes);
        System.out.println("Pranesimas nesuklastotas : " + ok);
    }
    
    public static void doElGamalDecrypt() throws Exception {
        BigInteger g256 = new BigInteger(
            "3F798DAD0EA7742FB9FE821D556B1AF7B3E1747B4E88C483DC495CBFE0F4078E", 16);
        BigInteger p256 = new BigInteger(
            "00A791047E786375ECADB5AC4557AE26D0475425A1AA6C34A707214548E62931C3", 16);
        ElGamalParameterSpec  egSpec = new ElGamalParameterSpec(p256, g256); 

        BigInteger       ct = new BigInteger ("36D932E3A086C334FFED0194F381E2E830A25D04B95AA0CB1875AF4B7D159CE5A60EB2291FA870DA29F29726ECCC90BB8601F5A4CC8FE4CB6884DE39532DE30E", 16);
        byte[]           inputBytes = ct.toByteArray();
        Cipher	         cipher = Cipher.getInstance("ElGamal/None/PKCS1Padding", "BC");
        SecureRandom     random = new SecureRandom();

        KeyFactory      keyFactory = KeyFactory.getInstance("ElGamal", "BC");
        ElGamalPublicKeySpec pubKeySpec = new ElGamalPublicKeySpec(
                new BigInteger("3BF48147393A021C345267958223303310FA81EC08ABDFD09C922ED91CE53DFE", 16),
                egSpec);
        ElGamalPrivateKeySpec privKeySpec = new ElGamalPrivateKeySpec(
                new BigInteger("6BA1A8CEB02B1478D59C15305A6979E2B38871912356CAB58D63B263738A1BCC", 16),
                egSpec);
        ElGamalPublicKey pubEG = (ElGamalPublicKey)keyFactory.generatePublic(pubKeySpec);
        ElGamalPrivateKey privEG = (ElGamalPrivateKey)keyFactory.generatePrivate(privKeySpec);

        System.out.println("Duotoji sifrograma : " + toHex(inputBytes));
        cipher.init(Cipher.DECRYPT_MODE, privEG);
        byte[] plainText = cipher.doFinal(inputBytes, 0, inputBytes.length);

        System.out.println("Iššifruota tekstograma : " + toHex(plainText));

        //patikrinimas
        cipher.init(Cipher.ENCRYPT_MODE, pubEG, random);
        byte[] cipherText = cipher.doFinal(plainText);

        System.out.println("Vel uzsifruotas : " + toHex(cipherText));

        System.out.println("EG viesas Y : " + toHex(pubEG.getY().toByteArray()));
        System.out.println("EG privatus X : " + toHex(privEG.getX().toByteArray()));
        System.out.println("EG generatorius G : " + toHex(privEG.getParameters().getG().toByteArray()));
        System.out.println("EG modulis P : " + toHex(pubEG.getParameters().getP().toByteArray()));
        //Ar turi sutapti? Kodel?
    }
    
    public static void doElGamalDecryptMOD() throws Exception {
        BigInteger g256 = new BigInteger(
            "3F798DAD0EA7742FB9FE821D556B1AF7B3E1747B4E88C483DC495CBFE0F4078E", 16);
        BigInteger p256 = new BigInteger(
            "00A791047E786375ECADB5AC4557AE26D0475425A1AA6C34A707214548E62931C3", 16);
        ElGamalParameterSpec  egSpec = new ElGamalParameterSpec(p256, g256); 

        BigInteger       ct = new BigInteger ("37F932E3A086C334FFED0194F381E2E830A25D04B95AA0CB1875AF4B7D159CE5A60EB2291FA870DA29F29726ECCC90BB8601F5A4CC8FE4CB6884DE39532DE30E", 16);
        byte[]           inputBytes = ct.toByteArray();
        Cipher	         cipher = Cipher.getInstance("ElGamal/None/PKCS1Padding", "BC");
        SecureRandom     random = new SecureRandom();

        KeyFactory      keyFactory = KeyFactory.getInstance("ElGamal", "BC");
        ElGamalPublicKeySpec pubKeySpec = new ElGamalPublicKeySpec(
                new BigInteger("3BF48147393A021C345267958223303310FA81EC08ABDFD09C922ED91CE53DFE", 16),
                egSpec);
        ElGamalPrivateKeySpec privKeySpec = new ElGamalPrivateKeySpec(
                new BigInteger("6BA1A8CEB02B1478D59C15305A6979E2B38871912356CAB58D63B263738A1BCC", 16),
                egSpec);
        ElGamalPublicKey pubEG = (ElGamalPublicKey)keyFactory.generatePublic(pubKeySpec);
        ElGamalPrivateKey privEG = (ElGamalPrivateKey)keyFactory.generatePrivate(privKeySpec);

        System.out.println("Duotoji sifrograma : " + toHex(inputBytes));
        cipher.init(Cipher.DECRYPT_MODE, privEG);
        byte[] plainText = cipher.doFinal(inputBytes, 0, inputBytes.length);

        System.out.println("Iššifruota tekstograma : " + toHex(plainText));

        //patikrinimas
        cipher.init(Cipher.ENCRYPT_MODE, pubEG, random);
        byte[] cipherText = cipher.doFinal(plainText);

        System.out.println("Vel uzsifruotas : " + toHex(cipherText));

        System.out.println("EG viesas Y : " + toHex(pubEG.getY().toByteArray()));
        System.out.println("EG privatus X : " + toHex(privEG.getX().toByteArray()));
        System.out.println("EG generatorius G : " + toHex(privEG.getParameters().getG().toByteArray()));
        System.out.println("EG modulis P : " + toHex(pubEG.getParameters().getP().toByteArray()));
        //Ar turi sutapti? Kodel?
    }
}
// import { CurrentUser } from "../types/types";

// // helper encryption library
// import Crypto from "crypto";

// // encryption secret key, ideally should be an environment variable
// const secret = "secret";

// // expiry duration in milliseconds, ensure you calculate the expiry in milliseconds
// const expiryDuration = 1680000;

// // encrypt data
// const encrypt = (data: CurrentUser) => {
//     if (data != null) {
//         return Crypto.encrypt(
//             JSON.stringify(data),
//             secret
//         ).toString();
//     }
//     return null;
// };

// // decrypt encrypted data
// const decrypt = (ciphertext: any) => {
//     try {
//         if (
//             ciphertext != null &&
//             ciphertext !== "null"
//         ) {
//             let bytes = CryptoJS.AES.decrypt(ciphertext.toString(), secret);
//             let decrypted = bytes.toString(CryptoJS.enc.Utf8);
//             return JSON.parse(decrypted);
//         }
//         return null;
//     } catch (e) {
//         return null;
//     }
// };

// // store in localStorage
// const store = (key, value) => {
//     return localStorage.setItem(key, value);
// };

// // read from localstorage
// const read = (key: string) => {
//     return localStorage.getItem(key);
// };

// // get new expiry
// const getExpiry = () => {
//     return (new Date().getTime() + expiryDuration);
// };

// // check if expired
// const isExpired = (expiry: string) => {
//     return (new Date().getTime() > parseInt(expiry, 10));
// };

// // Encrypt and store with time expiry functionality
// const storeExpiry = (key, value, expiry = false) => {
//     const encryptedData = encrypt(value);
//     if (expiry === true) {
//         const encryptedExpiry = encrypt(getExpiry());
//         store(`${key}.e`, encryptedExpiry);
//     }
//     return store(key, encryptedData);
// };

// // decrypt and read with time expiry functionality
// const readExpiry = key => {
//     const expiryData = decrypt(read(`${key}.e`));
//     const data = decrypt(read(key));
//     if (data != null) {
//         if (data && isExpired(expiryData)) {
//             return { response: data, expired: true };
//         }
//         if (data && !isExpired(expiryData)) {
//             return { response: data, expired: false };
//         }
//     }
//     return { response: null, expired: true };
// };

// // reset localstorage
// const clear = () => {
//     localStorage.clear();
//     return null;
// };

// module.exports = { encrypt, decrypt, clear, storeExpiry, readExpiry, read, store };
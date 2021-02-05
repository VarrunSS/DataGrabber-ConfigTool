"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function getUserToken() {
    return localStorage.getItem("jwt");
}
exports.getUserToken = getUserToken;
function removeUserToken() {
    return localStorage.removeItem("jwt");
}
exports.removeUserToken = removeUserToken;
function setUserToken(token) {
    return localStorage.setItem('jwt', token);
}
exports.setUserToken = setUserToken;
//# sourceMappingURL=getUserToken.js.map
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
exports.getBaseUrl = getBaseUrl;
/**
 * Provider POJO for the Base Url
 */
exports.BaseUrlProvider = {
    provide: 'BASE_URL',
    useFactory: getBaseUrl
};
//# sourceMappingURL=getBaseUrl.js.map
"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
/* CLASSES */
var ConfigurationDetail = /** @class */ (function () {
    function ConfigurationDetail() {
        this.siteConfiguration = new SiteConfiguration();
        this.websiteDetail = new WebsiteDetail();
        this.productDetail = new ProductDetail();
        this.paginationDetail = new PaginationDetail();
        this.mailingInformation = new MailingInformation();
    }
    return ConfigurationDetail;
}());
exports.ConfigurationDetail = ConfigurationDetail;
var SiteConfiguration = /** @class */ (function () {
    function SiteConfiguration() {
    }
    return SiteConfiguration;
}());
exports.SiteConfiguration = SiteConfiguration;
var WebsiteDetail = /** @class */ (function () {
    function WebsiteDetail() {
        this.inputFields = [];
    }
    return WebsiteDetail;
}());
exports.WebsiteDetail = WebsiteDetail;
var ProductDetail = /** @class */ (function () {
    function ProductDetail() {
        this.fields = [];
    }
    return ProductDetail;
}());
exports.ProductDetail = ProductDetail;
var PaginationDetail = /** @class */ (function () {
    function PaginationDetail() {
    }
    return PaginationDetail;
}());
exports.PaginationDetail = PaginationDetail;
var MailingInformation = /** @class */ (function () {
    function MailingInformation() {
    }
    return MailingInformation;
}());
exports.MailingInformation = MailingInformation;
var BaseField = /** @class */ (function () {
    function BaseField() {
    }
    return BaseField;
}());
exports.BaseField = BaseField;
var InputField = /** @class */ (function (_super) {
    __extends(InputField, _super);
    function InputField() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return InputField;
}(BaseField));
exports.InputField = InputField;
var Field = /** @class */ (function (_super) {
    __extends(Field, _super);
    function Field() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return Field;
}(BaseField));
exports.Field = Field;
//# sourceMappingURL=ConfigurationDetail.js.map
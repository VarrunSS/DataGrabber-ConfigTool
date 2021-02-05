
export interface IBaseConfigurationDetail {
  configGUID?: string;
  configName?: string;
  configType?: string;
  url?: string;
  createdBy?: string;
  createdOn?: string;
}

export interface IConfigurationDetail extends IBaseConfigurationDetail {
  siteConfiguration: ISiteConfiguration;
  websiteDetail: IWebsiteDetail;
  productDetail: IProductDetail;
  paginationDetail: IPaginationDetail;
  mailingInformation: IMailingInformation;
}

export interface ISiteConfiguration {
  websiteConfigurationName?: string;
  scrapingMechanism: string;
  shouldRotateProxy?: number;
  requireInputValues?: number;
  shouldDisableJavaScript?: number;
  waitingTimeAfterPageLoad?: string;
}
export interface IWebsiteDetail {
  websiteNamePrefix?: string;
  webscrapeType: string;
  websiteURL?: string;
  websiteURLs?: string[];
  doesHaveSearchButton?: number;
  searchButtonPathType?: string;
  searchButton?: string;
  doesHaveResetButton?: number;
  resetButtonPathType?: string;
  resetButton?: string;

  inputFields?: IInputField[];
}
export interface IProductDetail {
  overallContainerPathType?: string;
  overallContainer?: string;
  productContainerPathType?: string;
  productContainer?: string;

  fields?: IField[];
}
export interface IPaginationDetail {
  pagingType?: string;
  paginationContainerPathType?: string;
  paginationContainer?: string;
  doesHaveNextButton?: number;
  nextButtonPathType?: string;
  nextButton?: string;
  activeClassForCurrentPage?: string;
  disabledClassForLastPage?: string;
  shouldLimitPaging?: number;
  pagingLimit?: string;
}
export interface IMailingInformation {
  shouldSendMail?: number;
  mailToAddress?: string;
  mailCCAddress?: string;
  mailBCCAddress?: string;

}


export interface IBaseField {
  fieldName?: string;
  fieldPathType?: string;
  fieldPath?: string;
}
export interface IInputField extends IBaseField {
  targetType?: string;
  hasPartnerElement: number;
  waitingTimeAfterElementChange?: string;
  isCollapsed?: number;
}

export interface IField extends IBaseField {
  shouldCheckElementInBody?: number;
  removeText?: string;
  attributeName?: string;
  isCollapsed?: number;
}



/* CLASSES */

export class ConfigurationDetail implements IConfigurationDetail {
  constructor() {
    this.siteConfiguration = new SiteConfiguration();
    this.websiteDetail = new WebsiteDetail();
    this.productDetail = new ProductDetail();
    this.paginationDetail = new PaginationDetail();
    this.mailingInformation = new MailingInformation();
  }
  siteConfiguration: ISiteConfiguration;
  websiteDetail: IWebsiteDetail;
  productDetail: IProductDetail;
  paginationDetail: IPaginationDetail;
  mailingInformation: IMailingInformation;
}


export class SiteConfiguration implements ISiteConfiguration {
  websiteConfigurationName?: string;
  scrapingMechanism: string;
  shouldRotateProxy?: number;
  requireInputValues?: number;
  shouldDisableJavaScript?: number;
  waitingTimeAfterPageLoad?: string;
}
export class WebsiteDetail implements IWebsiteDetail {
  constructor() {
    this.inputFields = [];
  }
  websiteNamePrefix?: string;
  webscrapeType: string;
  websiteURL?: string;
  websiteURLs?: string[];
  doesHaveSearchButton?: number;
  searchButtonPathType?: string;
  searchButton?: string;
  doesHaveResetButton?: number;
  resetButtonPathType?: string;
  resetButton?: string;

  inputFields?: InputField[];
}
export class ProductDetail implements IProductDetail {
  constructor() {
    this.fields = [];
  }
  overallContainerPathType?: string;
  overallContainer?: string;
  productContainerPathType?: string;
  productContainer?: string;

  fields?: Field[];
}
export class PaginationDetail implements IPaginationDetail {
  pagingType?: string;
  paginationContainerPathType?: string;
  paginationContainer?: string;
  doesHaveNextButton?: number;
  nextButtonPathType?: string;
  nextButton?: string;
  activeClassForCurrentPage?: string;
  disabledClassForLastPage?: string;
  shouldLimitPaging?: number;
  pagingLimit?: string;
}
export class MailingInformation implements IMailingInformation {
  shouldSendMail?: number;
  mailToAddress?: string;
  mailCCAddress?: string;
  mailBCCAddress?: string;

}


export class BaseField implements IBaseField {
  fieldName?: string;
  fieldPathType?: string;
  fieldPath?: string;
}
export class InputField extends BaseField implements IInputField {
  targetType?: string;
  hasPartnerElement: number;
  waitingTimeAfterElementChange?: string;
  isCollapsed?: number;
}

export class Field extends BaseField implements IField {
  shouldCheckElementInBody?: number;
  removeText?: string;
  attributeName?: string;
  isCollapsed?: number;
}

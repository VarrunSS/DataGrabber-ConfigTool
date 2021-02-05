
export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

/**
 * Provider POJO for the Base Url
 */
export const BaseUrlProvider = {
  provide: 'BASE_URL',
  useFactory: getBaseUrl
};


export function getUserToken() {
  return localStorage.getItem("jwt");
}

export function removeUserToken() {
  return localStorage.removeItem("jwt");
}

export function setUserToken(token: string) {
  return localStorage.setItem('jwt', token);
}

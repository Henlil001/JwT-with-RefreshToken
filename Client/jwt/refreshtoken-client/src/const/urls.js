const apiBaseUrl = import.meta.env.VITE_API_BASE_URL;

class urls {
  static login = apiBaseUrl + "auth/login";
  static logout = apiBaseUrl + "auth/logout";
  static refreshtoken = apiBaseUrl + "auth/refresh";
  static createUser = apiBaseUrl + "auth/create-user";

  static allProducts = apiBaseUrl + "products";
}
export default urls;

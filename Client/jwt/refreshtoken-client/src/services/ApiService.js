import { refreshtoken } from "./AuthService";

export async function fetchWithAuth(url, options = {}) {
  let accessToken = localStorage.getItem("accessToken");

  options.headers = {
    ...options.headers,
    "Content-Type": "application/json",
  };

  if (accessToken) {
    options.headers.Authorization = `Bearer ${accessToken}`;
  }

  let response = await fetch(url, {
    ...options,
    credentials: "include", // skickar refresh-token-cookien
  });

  // Om accessToken har gått ut
  if (response.status === 401) {
    try {
      const response = await refreshtoken();
      const data = response.json();
      if (response.status === 200 && data.accessToken) {
        localStorage.setItem("accessToken", data.accessToken);
        accessToken = data.accessToken;

        options.headers.Authorization = `Bearer ${accessToken}`;
        response = await fetch(url, {
          ...options,
          credentials: "include",
        });
      }
      else{
        localStorage.removeItem("accessToken");
        window.location.href = "/"; // logga ut användaren
      }
    } catch (err) {
      console.error("Refresh token failed", err);
      localStorage.removeItem("accessToken");
      window.location.href = "/"; // logga ut användaren
    }
  }

  return response;
}
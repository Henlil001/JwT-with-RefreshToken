import { fetchWithAuth } from "./ApiService";
import urls from "../const/urls";

export const GetAllProducts = async () => {
  const response = await fetchWithAuth(urls.allProducts, {
    method: "POST",
  });

  if (!response.ok) {
    throw new Error("Failed to fetch products");
  }

  return await response.json();
};
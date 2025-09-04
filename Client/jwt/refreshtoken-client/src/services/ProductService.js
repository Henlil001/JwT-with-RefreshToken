import urls from "../const/urls";

export const GetAllProducts = async ()=>{
    const response = await fetch(urls.allProducts, {
      method: "post",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("accesstoken")}`
      },
      credentials: "include",
    }).then(response.json(data));
    return data;
    
}
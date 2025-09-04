import urls from "../const/urls";

export const RefreshToken = async () => {
  try {
    const response = await fetch(urls.refreshtoken, {
      method: "post",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    if (response.status === 500) {
       throw new Error("Server error");
    }
    return response;
  } catch (error) {
    console.error("refresh failed", error);
    throw error;
  }
};

export const LogOut = async ()=>{
   try {
    const response = await fetch(urls.logout, {
      method: "post",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    if (response.status === 500) {
       throw new Error("Server error");
    }
    return response;
  } catch (error) {
    console.error("logout failed", error);
    throw error;
  }
}

export const Login = async ({loginrequest}) => {
  try {
    const response = await fetch(urls.login, {
      method: "post",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ loginrequest }),
    });
    if (response.status === 500) {
      throw new Error("Server error");
    }
    return response;
  } catch (error) {
    console.error("login failed", error);
    throw error;
  }
};

export const CreateUser = async ({ user }) => {
  try {
    const response = await fetch(urls.login, {
      method: "post",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ user }),
    });
    if (response.status === 500) {
      throw new Error("Server error");
    }
    return response;
  } catch (error) {
    console.error("login failed", error);
    throw error;
  }
};

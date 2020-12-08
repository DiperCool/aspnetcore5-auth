import React, { useState } from "react";
import { Redirect } from "react-router-dom";
import { sendReq } from "../../Api/Auth/sendReq";

export const GetLogin=()=>{

    let [login,setLogin]= useState("");

    const GetLogin=async()=>{
        let res= await sendReq("get", "api/auth/getLogin")
        setLogin(res.data)

    }
    return(
        <div>
            {login}
            <button onClick={GetLogin}>Получить</button>
        </div>
    )
}
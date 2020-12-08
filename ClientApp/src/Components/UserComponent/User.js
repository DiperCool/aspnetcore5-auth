import React, { useState, useEffect } from "react";
import {UserContext} from "./UserContext";
import {sendReq} from "../../Api/Auth/sendReq";
import { Redirect } from "react-router-dom";
export const User=({children})=>{

    let [Auth, setAuth]= useState({
        login:"",
        isAuth:false,
        isPending:true,
    });


    const setGetLogin=async()=>{
        setAuth({isPending: true})
        var result=await sendReq("get", "api/auth/getLogin")
        if(result.status===200){
            setAuth({
                login:result.data,
                isAuth:true,
                isPending:false,
            })
            return;
        }
        setAuth({
            login:"",
            isAuth:false,
            isPending:false
        });
    }

    useEffect(()=>{
        setGetLogin();
    },[]);


    const setLoginAndSetAuth=(login)=>{
        setAuth({
            login:login,
            isAuth:true,
            isPending:false
        });
    }


    if(Auth.isPending) return <div>загрузка</div>;

    return(
        <UserContext.Provider value={{Auth,setLoginAndSetAuth,setGetLogin}}>
            {children}
        </UserContext.Provider>
    )
}
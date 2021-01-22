import React, {useRef, useState, useContext} from "react";
import {Redirect} from "react-router-dom";
import Auth from "../../Api/Auth/Auth";
import { UserContext } from "../UserComponent/UserContext";
import { GoogleButton } from "./GoogleButton";
export const Register=()=>{

    let [Errors, setErrors]=useState({
        isErrors:false,
        allErrors:[]
    });
    let {setGetLogin}= useContext(UserContext);
    let refEmeil = useRef(null);
    let refPassword = useRef(null);
    let refRePassword = useRef(null);
    let refFirstName = useRef(null);
    let refLastName = useRef(null);

    let handler=async()=>{
        let obj={
            email: refEmeil.current.value,
            password: refPassword.current.value,
            rePassword: refRePassword.current.value,
            firstName: refFirstName.current.value,
            lastName: refLastName.current.value
        }
        let result= await Auth.register(obj);
        if(result.notSuccesed){
            setErrors({
                isErrors:true,
                allErrors:result.errors
            })
            return;
        }
        setGetLogin();



    }

    return(
            <div>
                <div>
                    <input type={"text"} ref={refFirstName} placeholder={"Введите имя"}></input>
                    <input type={"text"} ref={refLastName} placeholder={"Введите фамилию"}></input>
                </div>
                <div>
                    <input type={"email"} ref={refEmeil} placeholder={"Введите почту"}></input>
                </div>
                <br></br>
                <div>
                    <input type={"password"} ref={refPassword} placeholder={"Введите пароль"}></input>
                    <input type={"password"} ref={refRePassword} placeholder={"Повторите пароль"}></input>
                </div>
                <br></br>
                <button onClick={handler}>Зарегестрироваться</button>
                <GoogleButton/>
            </div>

    )
}
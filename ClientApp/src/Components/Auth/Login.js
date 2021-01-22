import React,{useRef, useState, useContext} from "react";
import {Redirect} from "react-router-dom";
import Auth from "../../Api/Auth/Auth";
import {UserContext} from "../UserComponent/UserContext";

export const Login=()=>{
    let [isOk, setOk]= useState(false);
    let [Errors, setErrors]=useState({
        isErrors:false,
        allErrors:[]
    });
    let refEmail= useRef(null);
    let refPassword=useRef(null);

    let {setGetLogin}= useContext(UserContext);

    const handler=async()=>{
        let obj= {
            email: refEmail.current.value,
            password: refPassword.current.value
        }
        let result=await Auth.login(obj);
        if(result.notSuccesed){
            setErrors({
                isErrors:true,
                allErrors:[result.errors]
            })
            return;
        }
        setGetLogin()
        setOk(true);
    }

    if(isOk){
        return <Redirect to="/profile"/>
    }

    return (
        <div>
            <div>
                <input 
                    ref={refEmail}
                    type="email" 
                    id="standard-basic" 
                    placeholder="Email" />
                <br></br>
                <input 
                    ref={refPassword} 
                    id="standard-basic" 
                    placeholder="Password" 
                    type="password"
                        />
                <br></br>
                <button onClick={handler}>Войти</button>
            </div>
        </div>
    )
}
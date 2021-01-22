import React from 'react';
import ReactDOM from 'react-dom';
// or
import { GoogleLogin } from 'react-google-login';
import jwt from '../../Api/Auth/ControlJwt';
import { sendReq } from '../../Api/Auth/sendReq';
import { config } from '../../config';


export const GoogleButton=()=>{
    const responseGoogle = async (response) => {
        if(!response.tokenId){
            console.log("ошибка");
            return;
        }
        let res= await sendReq("POST", "api/auth/signinGoogle", {
            data:{"tokenId":response.tokenId},
            headers:{"Content-Type":"application/json"}
        });
        jwt.setJwt(res.data.token);
        jwt.setRefreshToken(res.data.refreshToken)
        console.log(res.data.token);
        console.log(res.data.refreshToken);
      }
      
      return (
        <GoogleLogin
          clientId="121658311073-612jp1vbps10cbj56htddm1lbp0nob5t.apps.googleusercontent.com"
          buttonText="Login"
          onSuccess={responseGoogle}
          onFailure={responseGoogle}
        />)
}

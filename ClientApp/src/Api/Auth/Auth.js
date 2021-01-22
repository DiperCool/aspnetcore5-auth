import axios from "axios";
import jwt from "./ControlJwt";
import {config} from "../../config";
class Auth{
    async login(obj){
        try{
            let result= await axios.post(config.url+"api/auth/login",obj);
            jwt.setJwt(result.data.token)
            jwt.setRefreshToken(result.data.refreshToken);
            return {
                notSuccesed: false
            }
        }
        catch(result){
            return {
                notSuccesed: true,
                errors:result.response.data
            }
        }
    }


    async register(obj){
        try{
            let result = await axios.post(config.url+"api/auth/register",obj);
            jwt.setJwt(result.data.token)
            jwt.setRefreshToken(result.data.refreshToken);
            return {
                notSuccesed: false
            }
        }
        catch(result){
            let errorsRes=result.response.data.errors;
            let arr=[];
            for(let obj in errorsRes){
                errorsRes[obj].map(el=>{
                    arr.push(el);
                    return el;
                })
            }
            if(result.response.data.LoginExcaption!=null){
                arr.push(result.response.data.LoginExcaption);
            }
            return {
                notSuccesed: true,
                errors:arr,
            }
        }
    }

    logout(){
        jwt.setJwt("")
        jwt.setRefreshToken("");
        window.location.href="/login";
    }

}

export default new Auth();
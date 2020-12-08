import React from 'react';
import { BrowserRouter, Switch, Redirect, Route } from 'react-router-dom';
import {Login} from "./Components/Auth/Login"
import { Register } from './Components/Auth/Register';
import {User} from "./Components/UserComponent/User"
import {PublicRoute} from "./Components/tools/PublicRoute";
import { PrivateRoute } from './Components/tools/PrivateRoute';
import { GetLogin } from './Components/Auth/GetLogin';
import { Logout } from './Components/Auth/Logout';
export const App=()=>{

  return(
    <User>
      <BrowserRouter>
        <Switch>

          <PublicRoute exact path="/login" component={Login}/>
          <PublicRoute exact path="/register" component={Register}/>

          <PrivateRoute exact path="/get" component={GetLogin}/>
          <PrivateRoute exact path="/logout" component={Logout}/>
          {/* <Redirect to={"/myprofile"}/> */}
        </Switch>
      </BrowserRouter>
    </User>
  )
}
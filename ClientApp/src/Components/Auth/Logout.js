import React from 'react';
import Auth from '../../Api/Auth/Auth';

export const Logout = () => {


    let onClick=()=>{
        Auth.logout()
    }

    return ( 
        <div>
            <button onClick={onClick}>Выйти</button>
        </div>
    );
}
 
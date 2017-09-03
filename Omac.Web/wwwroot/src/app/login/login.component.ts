import { Component } from '@angular/core'
//import { User } from '../../Models/User'

@Component({
    selector: 'login',
    templateUrl: './login.component.html'
})
export class LoginComponent {

    //public user: User = {
    //    email : 'kishorsanu1994@gmail.com',
    //    password : 'samsung44'
    //};

    login(): void {
        //console.log("User: " + this.user.email + " & Password: " + this.user.password);
    }

    logout(): void {

    }
}

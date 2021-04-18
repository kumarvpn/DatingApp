import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { from } from 'rxjs';
import {AccountService} from '../_Services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  models : any={}
  UserName :any
  constructor(public Httpservice:AccountService
    ) { }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  Login() {
    this.Httpservice.login(this.models).subscribe(Response => {

      console.log(Response);
     this.setCurrentUser();
    },
    
    Error=>{
        
      console.log(Error);

    });
    
  }
 setCurrentUser()
 {
   const user = JSON.parse(localStorage.getItem('user'));
   if (user) {
     this.UserName = user["userName"];
   }
 }

logout()
{
  this.Httpservice.logout();
}


// getCurrentUser(){
//   this.Httpservice.currentUser$.subscribe(user=>{
//     this.IsLoggedIn = !!user;
//   },Error=>{

//     console.log(Error);
//   });
   
// }


}

import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_Models/User';
import { AccountService } from './_Services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

 constructor (private http: HttpClient , private accoutservice : AccountService ){}
 users:any
 title = 'The Dating App';
  ngOnInit() {
   this.getUsers();
   this.setCurrentuser();
  }
  
  setCurrentuser()
  {
    const user :User =  JSON.parse(localStorage.getItem('user'));
    this.accoutservice.setCurrentUser(user);
  }


  getUsers()
  {
   this.http.get("https://localhost:5001/api/users").subscribe(Response=>{

this.users=Response;
   },Error=>{

    console.log(Error);
    
   });

  }

}

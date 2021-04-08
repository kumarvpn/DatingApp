import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

 constructor (private http: HttpClient ){}
 users:any
 title = 'The Dating App';
  ngOnInit() {
   this.getUsers();
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

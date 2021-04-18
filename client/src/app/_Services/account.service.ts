import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {User} from '../_Models/User';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl ="https://localhost:5001/api/";

  private CurrentUserSource = new  ReplaySubject<User>(1);
  currentUser$ = this.CurrentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + "Account/Login", model).pipe(
      map((Response: User) => {

        const user = Response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.CurrentUserSource.next(user);
        }
      })

    );

  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.CurrentUserSource.next(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    this.CurrentUserSource.next(user);
  }

  logout (){
    localStorage.removeItem('user')
    this.CurrentUserSource.next(null);
  }

}

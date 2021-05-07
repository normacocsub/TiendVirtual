import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: string = '';
  password: string = '';
  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
  }

  login(){
    this.loginService.login(this.user,this.password).pipe(
      (first())
    )
    .subscribe( result => {
      window.location.reload();
    },
    error =>{
      //mensaje de que no se logueo correctamente
    });
  }

}

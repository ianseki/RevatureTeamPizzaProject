import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../shared/user.service';
// import { ToastrService} from 'ngx-toastr';
@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  // loginForm!:FormGroup;
  formModel={
    UserName: '',
    Password: ''
  }

  // private formBuilder:FormBuilder, private _http:HttpClient, private router:Router add inside constructor
  constructor(private service: UserService, private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(){
    if (localStorage.getItem('token') != null)
    this.router.navigateByUrl('/header');
    // this.loginForm = this.formBuilder.group({
    //   email:[''],
    //   password:['']
    // })
  }
    
    onsubmit(form: NgForm){
      this.service.login(form.value).subscribe((res:any) => {
        localStorage.setItem('token', res.token);
        this.router.navigateByUrl('/header');

      },
      err => {
        if (err.status == 400)
          this.toastr.error("Incorrect username or password");

        else
          console.log(err);  
      }
      
      
      );
        
      

    }

  }  
    
    // NEED TO CONNECT TO API
  // logIn(){
  //   this._http.get<any>("https:localhost7044/CheckLogIn").subscribe(res=>{
  //     const user = res.find((a:any)=>{
  //       return a.email === this.loginForm.value.email && a.password === this.loginForm.value.password

  //     })
  //     if(user){
  //       alert("Login Succesfull!");
  //       this.loginForm.reset();
  //       this.router.navigate(['home'])
  //     }else{
  //       alert("User Not Found")
  //     }
  //   }, err=>{
  //     alert("Please Try Again")
  //   }
  //   )

  // }  


  
  



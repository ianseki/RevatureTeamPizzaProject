import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { APIService } from '../services/api.service';

// import { ToastrService} from 'ngx-toastr';
@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})

export class LoginPageComponent implements OnInit{

  LoginData= new FormGroup({
    email: new FormControl(''),
    password : new FormControl('')
  })
  LoginReponse: any;

  constructor(private API: APIService, private Router: Router) {}

  ngOnInit(): void { }

  FUNC_LogIn (){
    const STRING_Username: string = `${this.LoginData.value.email}`;
    const STRING_Password: string = `${this.LoginData.value.password}`;
    this.API.CUSTOMER_Login(STRING_Username, STRING_Password).subscribe((API_Response: Number) =>
    {
      this.LoginReponse = API_Response;

      if(this.LoginReponse == -1){
        alert("LOGIN FAILED, YOU NEED HELP");
      }
      else{
        const CustomerInfo: DATA_Customer = this.LoginReponse;
        alert("Login Succesfull!");
        this.Router.navigate(['home'])
      }
    })
  }
}

export interface DATA_Customer{
  customerid: number
}
// export class LoginPageComponent implements OnInit {

//   // loginForm!:FormGroup;
//   // formBuilder={
//   //   UserName: '',
//   //   Password: ''
//   // }
  
//   // private formBuilder:FormBuilder, private _http:HttpClient, private router:Router add inside constructor
//   constructor(private service: APIService, private router: Router, private toastr: ToastrService) { }

//   ngOnInit(): void{
//     if (localStorage.getItem('token') != null)
//     this.router.navigateByUrl('/header');
//     this.loginForm = this.formBuilder.group({
//       email:[''],
//       password:['']
//     })
//   }
    
//     logIn(form: NgForm){
//       this.service.CUSTOMER_Login().subscribe((res:any) => {
//         localStorage.setItem('token', res.token);
//         this.router.navigateByUrl('/header');

//       },
//       err => {
//         if (err.status == 400)
//           this.toastr.error("Incorrect username or password");

//         else
//           console.log(err);  
//       }
      
//       );
      
//     }

//   }  

    
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


  
  



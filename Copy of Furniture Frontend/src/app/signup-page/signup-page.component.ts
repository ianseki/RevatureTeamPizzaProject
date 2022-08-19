import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm} from '@angular/forms';
import { APIService } from '../services/api.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  styleUrls: ['./signup-page.component.css']
})

export class SignupPageComponent implements OnInit {

  NewUserData= new FormGroup({
    firstname: new FormControl(''),
    lastname: new FormControl(''),
    email: new FormControl(''),
    password : new FormControl('')
  })

  NewUserResponse: any;

  CreateUserResponse: any;

  constructor(private API: APIService, private Router: Router) { }

  ngOnInit(){ }

  FUNC_NewUser(){
    const STRING_Firstname: string = `${this.NewUserData.value.firstname}`
    const STRING_Lastname: string = `${this.NewUserData.value.lastname}`
    const STRING_Email: string = `${this.NewUserData.value.email}`;
    const STRING_Password: string = `${this.NewUserData.value.password}`;

    this.API.CUSTOMER_CreateNewUser(STRING_Firstname, STRING_Lastname, STRING_Email, STRING_Password).subscribe((API_Response: boolean) =>
    {
      alert("API_Response: " + API_Response)

      this.NewUserResponse = API_Response;

      if (this.NewUserResponse == true)
      {
        alert("Successfully Created New User");
        this.Router.navigate(['login'])
      }
      else
      {
        alert("Failed To Create New User, Please Try Again")
      }
    });
  }

}







// import { HttpClient } from '@angular/common/http';
// import { Component, OnInit } from '@angular/core';
// import { FormBuilder, FormGroup } from '@angular/forms';
// import { Router } from '@angular/router';

// @Component({
//   selector: 'app-signup-page',
//   templateUrl: './signup-page.component.html',
//   styleUrls: ['./signup-page.component.css']
// })
// export class SignupPageComponent implements OnInit {
//   signupForm!:FormGroup

//   private formBuilder: FormBuilder, private _http:HttpClient, private router:Router inside constructor
//   constructor() { }

//   ngOnInit(): void {
//     this.signupForm = this.formBuilder.group({
//       firstname:[''],
//       lastname:[''],
//       email:[''],
//       password:[''],
      
//     })
//   }

//   signUp(){
//     this._http.post<any>("https:localhost7044/CreateNewCustomer",this.signupForm.value).subscribe(res =>{
//       alert("Registration Successful!")
//       this.signupForm.reset();
//       this.router.navigate(['login'])
//     }, err=>
//       alert("Please Try Again")
//     )


//   }

// }

import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../shared/user.service';


@Component({
  selector: 'app-signup-page',
  templateUrl: './signup-page.component.html',
  styleUrls: ['./signup-page.component.css']
})
export class SignupPageComponent implements OnInit {
  constructor(public service: UserService, private toastr: ToastrService){ }

  ngOnInit(){
    this.service.formModel.reset();
  }

  onSubmit(){
    this.service.register().subscribe(
      (res:any)=> {
        if (res.succeeded) {
          this.service.formModel.reset();
          this.toastr.success('Registration Successful!');
        } else {
          res.errors.forEach((element: { description: any; }) => {
            this.toastr.error(element.description, 'Registration Failed');
          }
          );
        }
      },
      err => {
        console.log(err);
      }

    )
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

import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class APIService {

  constructor(private http:HttpClient) { }

  // CUSTOMER FUNCTIONS
  public CUSTOMER_Login(INPUT_Email: string, INPUT_Password: string): Observable<number>{
    const API_URL = 'https://woodcutters-union-team-pizza.azurewebsites.net/API/Customer/CheckLogin';
    const API_Parameters = new HttpParams()
      .set("INPUT_Email", INPUT_Email)
      .set("INPUT_Password", INPUT_Password);

    return this.http.get<number>(API_URL, {'params': API_Parameters})
  }

  public CUSTOMER_CreateNewUser(INPUT_Firstname: string, INPUT_Lastname: string, INPUT_Email: string, INPUT_Password: string): Observable<boolean>{
    const API_URL = 'https://woodcutters-union-team-pizza.azurewebsites.net/API/Customer/CreateNewCustomer';

    const API_Body: DMODEL_Customer = {
      customer_id: 1, 
      first_name: INPUT_Firstname, 
      last_name: INPUT_Lastname, 
      email: INPUT_Email, 
      password: INPUT_Password}

    return this.http.post<boolean>(API_URL, API_Body)
  }

  public CUSTOMER_CreateNewOrder(INPUT_CustomerID: number, INPUT_DTO_OrderProject: DTO_OrderProject): Observable<any>{
    const API_URL = 'https://woodcutters-union-team-pizza.azurewebsites.net/API/Customer/CreateNewCustomer';
    const API_Parameters = new HttpParams()
      .set("INPUT_CustomerID", INPUT_CustomerID)

    return this.http.post(API_URL, INPUT_DTO_OrderProject, {'params': API_Parameters})
  }

  public CUSTOMER_GetOrderHistory(INPUT_CustomerID: number): Observable<any>{
    const API_URL = 'https://woodcutters-union-team-pizza.azurewebsites.net/API/Customer/GetOrderHistory';
    const API_Parameters = new HttpParams()
      .set("INPUT_CustomerID", INPUT_CustomerID)

    return this.http.get(API_URL, {'params': API_Parameters})
  }

  // EMPLOYEE FUNCTIONS
  public EMPLOYEE_Login(INPUT_Email: string, INPUT_Password: string): Observable<number>{
    const API_URL = 'https://woodcutters-union-team-pizza.azurewebsites.net/API/Employee/CheckLogin';
    const API_Parameters = new HttpParams()
      .set("INPUT_Email", INPUT_Email)
      .set("INPUT_Password", INPUT_Password);

    return this.http.get<number>(API_URL, {'params': API_Parameters})
  }

  public EMPLOYEE_CreateNewUser(INPUT_Firstname: string, INPUT_Lastname: string, INPUT_Email: string, INPUT_Password: string): Observable<boolean>{
    const API_URL = 'https://woodcutters-union-team-pizza.azurewebsites.net/API/Customer/CreateNewEmployee';
    
    const API_Body: DMODEL_Employee = {
      employee_id: 1, 
      first_name: INPUT_Firstname, 
      last_name: INPUT_Lastname, 
      email: INPUT_Email, 
      password: INPUT_Password}

    return this.http.post<boolean>(API_URL, API_Body)
  }

  public EMPLOYEE_UpdateProjectStatus(INPUT_ProjectID: number, INPUT_ProjectStatus: boolean): Observable<number>{
    const API_URL = 'https://woodcutters-union-team-pizza.azurewebsites.net/API/Employee/UpdateProjectStatus';
    const API_Parameters = new HttpParams()
      .set("INPUT_ProjectID", INPUT_ProjectID)
      .set("INPUT_ProjectStatus", INPUT_ProjectStatus);

    return this.http.post<number>(API_URL, {'params': API_Parameters})
  }

  public EMPLOYEE_GetOutstandingProject(INPUT_EmployeeID: number): Observable<Array<number>>{
    const API_URL = 'https://woodcutters-union-team-pizza.azurewebsites.net/API/Employee/GetOutstandingProject';
    const API_Parameters = new HttpParams()
      .set("INPUT_EmployeeID", INPUT_EmployeeID)

    return this.http.get<Array<number>>(API_URL, {'params': API_Parameters})
  }
}

export interface DMODEL_Customer{
  "customer_id": number,
  "first_name": string,
  "last_name": string,
  "email": string,
  "password": string
}

export interface DMODEL_Employee{
  "employee_id": number,
  "first_name": string,
  "last_name": string,
  "email": string,
  "password": string
}

export interface DMODEL_Order{
  "order_id": number,
  "time_of_order": string,
  "status": boolean,
}

export interface DMODEL_Project{
  "project_id": number,
  "item_id": number ,
  "completion_status": boolean
}

export interface DTO_OrderProject{
  "DMODEL_Order": DMODEL_Order
  "LIST_DMODEL_Projects": Array<DMODEL_Project>
}
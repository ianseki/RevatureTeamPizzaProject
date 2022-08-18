import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { identifierName } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private _http:HttpClient) { }

  postFurniture(data:any){
    return this._http.post<any>("https://localhost:7044/posts", data).pipe(map((res:any)=>{
      return res;
    }))
  }

  getFurniture(data:any, id:number){
    return this._http.get<any>("https://localhost:7044/posts"+id,data).pipe(map((res:any)=> {
      return res;
    }))
  }
}

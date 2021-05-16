import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router'; 
@Component({
  selector: 'app-wine-form',
  templateUrl: './wine-form.component.html',
  styleUrls: ['./wine-form.component.css']
})

export class WineFormComponent implements OnInit {

  public vines: Vine[];
  public vtypes: VineType[];
  public selectedType: VineType;

  public nameOfWine: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
 
  }

  public deleteWine(id) {
 
  }



  public addWine(values) {
    var vine: Vine = {
      id: 0,
      name: values.imeVina,
      wineTypeId: values.vrstaVina,
    }
    
    this.http.post<Vine>(this.baseUrl + "api/wines", vine).subscribe(result => {
      console.log(result)
      this.router.navigate(['/wines']);
    }, error => console.error(error));
  };

  ngOnInit() {
    this.http.get<Vine[]>(this.baseUrl + 'api/wines').subscribe(result => {
      this.vines = result;
    }, error => console.error(error));
    this.http.get<VineType[]>(this.baseUrl + 'api/wineTypes').subscribe(result => {
      this.vtypes = result;
    }, error => console.error(error));
  }

}

interface Vine {
  id: number;
  name: string;
  wineTypeId: number;
}

interface VineType {
  id: number;
  name: string;
  description: string;
}

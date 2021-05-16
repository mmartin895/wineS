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
    http.get<Vine[]>(baseUrl + 'api/wines').subscribe(result => {
      this.vines = result;
    }, error => console.error(error));
    http.get<VineType[]>(baseUrl + 'api/wineTypes').subscribe(result => {
      this.vtypes = result;
    }, error => console.error(error));
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
    }, error => console.error(error));
    this.router.navigate(['/wines']);

  };

  ngOnInit() {
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

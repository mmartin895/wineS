import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-wines',
  templateUrl: './wines.component.html'
})
export class WinesComponent implements OnInit {

  public vines: Vine[];

  public loadWines() {
    this.http.get<Vine[]>(this.baseUrl + 'api/wines').subscribe(result => {
      this.vines = result;
    }, error => console.error(error));
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public deleteWine(id) {

    this.http.delete(this.baseUrl + "api/wines/" + id).subscribe(result => {
      console.log(result)
      this.loadWines()
    }, error => console.error(error));

   
  }
  ngOnInit() {
    this.loadWines()
  }

}

interface Vine {
  id: number;
  name: string;
}

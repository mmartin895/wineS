import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-wine-packages',
  templateUrl: './wine-packages.component.html',
  styles: []
})
export class WinePackagesComponent implements OnInit {

  public packages: WinePackage[];
  public packageTypes: WinePackageType[];
  public winesInPackage: WineInPackage[];
  public wines: Wine[];

  public addingPackage: boolean = false;
  public addingWineToPackage: boolean = false;
  public selectedWineInPackage: WineInPackage;

  public selectedPackage: WinePackage;
  public editPackage: WinePackage;

  private baseUrl: string;
  private addPackageForm = this.formBuilder.group({
    name: '',
    packageTypeId: ''
  });

  private editPackageForm = this.formBuilder.group({
    id: '',
    name: '',
    packageTypeId: ''
  });

  private editWineInPackageForm = this.formBuilder.group({
    packageId: '',
    wineId: '',
    quantity: ''
  });


  constructor(private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private formBuilder: FormBuilder){
    this.baseUrl = baseUrl;
  }

  private loadPackages (callb?: any) {
    this.http.get<WinePackage[]>(this.baseUrl + 'api/packages').subscribe(result => {
      this.packages = result;
      if (callb) {
        callb();
      }
    }, error => console.error(error));
 
  }

  private loadPackageTypes() {
    this.http.get<WinePackageType[]>(this.baseUrl + 'api/packagetypes').subscribe(result => {
      this.packageTypes = result;
    }, error => console.error(error));
  }

  private loadWines() {
    this.http.get<Wine[]>(this.baseUrl + 'api/wines').subscribe(result => {
      this.wines = result;
    }, error => console.error(error));
  }

  private loadWinesInPackage(winePackage: WinePackage) {
    this.http.get<WineInPackage[]>(this.baseUrl + `api/packages/${winePackage.id}/wines`).subscribe(result => {
      this.winesInPackage = result;
    }, error => console.error(error));
  }

  private showAddPackageForm(): void {
    this.onCancelEditPackage();
    this.addingPackage = true;
  }

  private showAddWineToPackageForm(): void {
    this.addingWineToPackage = true;
    this.editWineInPackageForm.reset();
    const packageId = this.selectedPackage.id;
    this.editWineInPackageForm.setValue({
      packageId: packageId,
      wineId: '',
      quantity: ''
    });
  }

  private onSubmitPackage(): void {

    const payload = {
      name: this.addPackageForm.value['name'],
      PackageTypeId: this.addPackageForm.value['packageTypeId']
    };
    this.http.post<WinePackage>(this.baseUrl + 'api/packages', payload).subscribe(result => { 
      this.loadPackages(() => {
        const paket = this.packages.find(p => p.id === result.id);
        this.selectPackage(paket);
      });
      this.selectedPackage = null;
    }, error => console.error(error));

    console.log(this.addPackageForm.value);
    this.addPackageForm.reset();
    this.addingPackage = false;
  }

  private onCancelAddPackage() {
    this.addPackageForm.reset();
    this.addingPackage = false;
  }

  private onAddWine(): void {
    const packageId = this.editWineInPackageForm.value['packageId'];
    const payload = {
      Packageid: packageId,
      Wineid: this.editWineInPackageForm.value['wineId'],
      Quantity: this.editWineInPackageForm.value['quantity']
    };

    this.http.put(this.baseUrl + `api/packages/${packageId}/addwine`, payload).subscribe(result => {
      this.loadWinesInPackage(this.selectedPackage);
    }, error => console.error(error));

    console.log(this.editWineInPackageForm.value);
    this.editWineInPackageForm.reset();
    this.addingWineToPackage = false;
  }

  private onCancelAddWine(): void {
    this.editWineInPackageForm.reset();
    this.addingWineToPackage = false;
  }

  private selectPackage(winePackage: WinePackage): void {
    this.onCancelAddPackage();
    this.onCancelEditPackage();
    this.selectedPackage = winePackage;
    this.loadWinesInPackage(winePackage);
  }

  
  private editWine(winePackage: WineInPackage): void {
    this.selectedWineInPackage = winePackage;
    this.editWineInPackageForm.setValue({
      packageId: winePackage.packageId,
      wineId: winePackage.wineId,
      quantity: winePackage.quantity
    });
  }

  private onCancelEditWine() {
    this.editWineInPackageForm.reset();
    this.selectedWineInPackage = null;
  }

  private onEditWine(): void {
    const packageId = this.editWineInPackageForm.value['packageId'];
    const payload = {
      Packageid: packageId,
      Wineid: this.editWineInPackageForm.value['wineId'],
      Quantity: this.editWineInPackageForm.value['quantity']
    };

    this.http.put(this.baseUrl + `api/packages/${packageId}/updatewine`, payload).subscribe(result => {
      this.loadWinesInPackage(this.selectedPackage);
    }, error => console.error(error));

    console.log(this.editWineInPackageForm.value);
    this.editWineInPackageForm.reset();
    this.selectedWineInPackage = null;
  }

  private onDeleteWine(winePackage: WineInPackage): void {
    this.onCancelAddPackage();
    this.onCancelEditPackage();
    const payload = {
      Packageid: winePackage.packageId,
      Wineid: winePackage.wineId
    };

    this.http.put(this.baseUrl + `api/packages/${winePackage.packageId}/removewine`, payload).subscribe(result => {
      this.loadWinesInPackage(this.selectedPackage);
    }, error => console.error(error));

    console.log(this.editWineInPackageForm.value);
    this.editWineInPackageForm.reset();
    this.selectedWineInPackage = null;
  }

  private onEditPackage(winePackage: WinePackage): void {
    this.onCancelAddPackage();
    this.editPackage = winePackage;
    this.selectedPackage = winePackage;
    this.loadWinesInPackage(winePackage);
    this.editPackageForm.setValue({
      id: winePackage.id,
      packageTypeId: winePackage.packageTypeId,
      name: winePackage.name
    });
  }

  private onSubmitEditPackage(): void {
    const id = this.editPackageForm.value['id'];
    const payload = {
      id: id,
      packageTypeId: this.editPackageForm.value['packageTypeId'],
      name: this.editPackageForm.value['name']
    };
 
    this.http.put(this.baseUrl + `api/packages/${payload.id}`, payload).subscribe(result => {
      this.loadPackages(() => {
        var packet = this.packages.find(p =>  p.id === id );
        this.selectedPackage = packet;   
      });
    }, error => console.error(error));

    console.log(this.editPackageForm.value);
    this.editPackageForm.reset();
    this.editPackage = null;
  }

  private onCancelEditPackage(): void {
    this.editPackageForm.reset();
    this.editPackage = null;
  }

  private onDeletePackage(winePackage: WinePackage) {
    this.http.delete(this.baseUrl + `api/packages/${winePackage.id}`).subscribe(result => {
      this.loadPackages();
    }, error => console.error(error));

  }

  ngOnInit() {
    this.loadPackages();
    this.loadPackageTypes();
    this.loadWines();
  }

}

interface WinePackage {
  id: number;
  name: string;
  packageTypeId: number;
  packageType: WinePackageType
}

interface WinePackageType {
  id: number;
  name: string;
}

interface WineInPackage {
  packageId: number;
  wineId: number;
  quantity: number;
  wine: Wine;
}

interface Wine {
  id: number;
  name: string;
}

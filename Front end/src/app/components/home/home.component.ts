import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DestinationType } from '../../enums/destination-type.enum';
import { SearchDestinationDto } from '../../dtos/search-destination.dto';

@Component({
  selector: 'app-home',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  searchDestinationForm: FormGroup;
  dto: SearchDestinationDto;

  destinationTypes: string[] = []; 
  backendErrors: string = ''; // To store backend validation errors
  
  constructor(
      private fb: FormBuilder
    ) {
      // Initialize the DTO
      this.dto = new SearchDestinationDto();
      this.destinationTypes = Object.values(DestinationType);
  
      this.searchDestinationForm = this.fb.group({
        country: ['', [Validators.required]],
        city: [''],
        region: [''],
        destinationType: ['', [Validators.required]],
      });
    }
    get country() {
      return this.searchDestinationForm.get('country')!;
    }
    get city() {
      return this.searchDestinationForm.get('city')!;
    }
    get region() {
      return this.searchDestinationForm.get('region')!;
    }
    get destinationType() {
      return this.searchDestinationForm.get('destinationType')!;
    }

    onSubmit() {
    
        if (this.searchDestinationForm.invalid) {
          return;
        }
        this.backendErrors = '';
    
        this.dto = this.searchDestinationForm.value;
        console.log(this.dto);
      }
}

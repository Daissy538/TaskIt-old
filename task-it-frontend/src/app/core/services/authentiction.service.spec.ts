/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AuthentictionService } from './authentiction.service';

describe('Service: Authentiction', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthentictionService]
    });
  });

  it('should ...', inject([AuthentictionService], (service: AuthentictionService) => {
    expect(service).toBeTruthy();
  }));
});

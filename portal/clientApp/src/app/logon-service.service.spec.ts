import { TestBed } from '@angular/core/testing';

import { LogonServiceService } from './logon-service.service';

describe('LogonServiceService', () => {
  let service: LogonServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LogonServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

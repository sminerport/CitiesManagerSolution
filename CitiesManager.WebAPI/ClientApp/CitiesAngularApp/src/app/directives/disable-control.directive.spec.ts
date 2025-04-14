import { NgControl } from '@angular/forms';
import { DisableControlDirective } from './disable-control.directive';

describe('DisableControlDirective', () => {
  it('should create an instance', () => {
    const mockNgControl: NgControl = {
      control: null
    } as NgControl;

    const directive = new DisableControlDirective(mockNgControl);
    expect(directive).toBeTruthy();
  });
});

import {UserDto} from './UserDto';

export interface AuthResponseDto {
  token: string;
  user: UserDto;
}

﻿using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserHotel : DomainEntityId
{
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    
    public Guid HotelId { get; set; }
    public Hotel? Hotel { get; set; }
}
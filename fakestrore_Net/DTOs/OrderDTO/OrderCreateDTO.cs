﻿namespace fakestrore_Net.DTOs.OrderDTO
{
    public class OrderCreateDTO
    {
        public int UserId { get; set; }
        public required OrderProductDTO OrderProduct { get; set; }
    }
}
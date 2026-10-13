// [dark-factory e2e] seeded by the acceptance test P4-AT4 (run 5211b98a): the test below calls the function this file
// adds, so it cannot pass on the base and passes on the head. Safe to delete.
pub fn double(x: u32) -> u32 { x * 2 }

#[cfg(test)]
mod tests {
    #[test]
    fn doubles() { assert_eq!(super::double(2), 4); }
}

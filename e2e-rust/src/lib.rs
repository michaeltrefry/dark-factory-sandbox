// [dark-factory e2e] seeded by the acceptance test P4-AT4: one test calls the function this file adds, the
// other passes on the base, so the gate must reject it.
pub fn double(x: u32) -> u32 { x * 2 }

#[cfg(test)]
mod tests {
    #[test]
    fn doubles() { assert_eq!(super::double(2), 4); }

    #[test]
    fn already_passes_on_the_base() { assert_eq!(1 + 1, 2); }
}
